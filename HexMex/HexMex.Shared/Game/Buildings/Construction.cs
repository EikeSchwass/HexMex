using System.Linq;
using CocosSharp;
using static System.Math;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public BuildingConstructionFactory ConstructionFactory { get; }

        public bool IsConstructing { get; private set; }
        public float PassedConstructionTime { get; private set; }

        public float Progress { get; private set; }

        public Construction(HexagonNode position, BuildingConstructionFactory buildingConstructionFactory, World world) : base(position, world)
        {
            ConstructionFactory = buildingConstructionFactory;
            ResourceDirector.AllIngredientsArrived += StartConstructing;
            ResourceDirector.RequestIngredients(ConstructionFactory.StructureDescription.ConstructionCost.ResourceTypes.ToArray());
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            drawNode.DrawSolidCircle(position, World.WorldSettings.HexagonMargin, ColorCollection.ConstructionBackgroundColor);
            drawNode.DrawSolidArc(position, World.WorldSettings.HexagonMargin, (float)(PI / 2), (float)(-Progress * PI * 2), ColorCollection.ConstructionProgressColor);
            drawNode.DrawSolidCircle(position, World.WorldSettings.HexagonMargin * 3 / 4, ColorCollection.ConstructionBackgroundColor);
        }

        public sealed override void Update(float dt)
        {
            base.Update(dt);
            if (!IsConstructing)
                return;
            PassedConstructionTime += dt;
            Progress = PassedConstructionTime / ConstructionFactory.StructureDescription.ConstructionTime;
            if (PassedConstructionTime >= ConstructionFactory.StructureDescription.ConstructionTime)
            {
                PassedConstructionTime = 0;
                IsConstructing = false;
                var structure = ConstructionFactory.CreateFunction(Position, World);
                World.StructureManager.RemoveStructure(this);
                World.StructureManager.CreateStrucuture(structure);
            }
            OnRequiresRedraw();
        }

        private void StartConstructing(ResourceDirector arg1, ResourceType[] arg2)
        {
            IsConstructing = true;
        }
    }
}