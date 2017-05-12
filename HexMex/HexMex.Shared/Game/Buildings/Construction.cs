using System.Linq;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public Construction(HexagonNode position, BuildingConstructionFactory buildingConstructionFactory, World world) : base(position, world, buildingConstructionFactory.BuildingInformation.ConstructionCost, Enumerable.Empty<ResourceType>())
        {
            ConstructionFactory = buildingConstructionFactory;
            World = world;
        }

        public BuildingConstructionFactory ConstructionFactory { get; }

        public bool IsConstructing { get; private set; }
        public float PassedConstructionTime { get; private set; }

        public float Progress { get; private set; }
        private World World { get; }

        public sealed override void Update(float dt)
        {
            base.Update(dt);
            if (!IsConstructing)
                return;
            PassedConstructionTime += dt;
            Progress = PassedConstructionTime / ConstructionFactory.BuildingInformation.ConstructionTime;
            if (PassedConstructionTime >= ConstructionFactory.BuildingInformation.ConstructionTime)
            {
                PassedConstructionTime = 0;
                IsConstructing = false;
                var structure = ConstructionFactory.CreateFunction(Position, World);
                World.StructureManager.RemoveStructure(this);
                World.StructureManager.CreateStrucuture(structure);
            }
            OnRequiresRedraw();
        }

        protected override void StartProduction()
        {
            base.StartProduction();
            IsConstructing = true;
            Progress = 0;
        }
    }
}