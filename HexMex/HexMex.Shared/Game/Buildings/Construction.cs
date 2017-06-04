using System;
using System.Linq;
using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public BuildingConstructionFactory ConstructionFactory { get; }

        public bool IsConstructing { get; private set; }
        public float PassedConstructionTime { get; private set; }

        private bool ResourcesRequested { get; set; }

        public event Action<Construction> ConstructionCompleted;

        public float Progress { get; private set; }

        public Construction(HexagonNode position, BuildingConstructionFactory buildingConstructionFactory, World world) : base(position, world, new StructureDescription("Construction", $"Constructs {buildingConstructionFactory.StructureDescription.Name}", buildingConstructionFactory.StructureDescription.ConstructionCost, buildingConstructionFactory.StructureDescription.ConstructionTime))
        {
            ConstructionFactory = buildingConstructionFactory;
            ResourceDirector.AllIngredientsArrived += StartConstructing;
        }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            drawNode.DrawCircle(position, radius, World.GameSettings.VisualSettings.ColorCollection.GrayNormal, World.GameSettings.VisualSettings.StructureBorderThickness, World.GameSettings.VisualSettings.ColorCollection.White);
        }

        public sealed override void Update(float dt)
        {
            base.Update(dt);
            if (!ResourcesRequested)
            {
                ResourcesRequested = true;
                ResourceDirector.RequestIngredients(ConstructionFactory.StructureDescription.ConstructionCost.ResourceTypes.ToArray(), null);
            }
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
                ConstructionCompleted?.Invoke(this);
            }
            OnRequiresRedraw();
        }

        private void StartConstructing(ResourceDirector arg1, ResourceType[] arg2)
        {
            IsConstructing = true;
        }
    }
}