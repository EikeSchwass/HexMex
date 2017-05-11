using System.Linq;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public Construction(HexagonNode position, BuildingConstructionFactory buildingConstructionFactory, ResourceManager resourceManager, HexagonManager hexagonManager, StructureManager structureManager) : base(position, resourceManager, hexagonManager, buildingConstructionFactory.BuildingInformation.ConstructionCost, Enumerable.Empty<ResourceType>())
        {
            ConstructionFactory = buildingConstructionFactory;
            StructureManager = structureManager;
        }

        public bool IsConstructing { get; private set; }
        public float PassedConstructionTime { get; private set; }

        public float Progress { get; private set; }
        public BuildingConstructionFactory ConstructionFactory { get; }
        private StructureManager StructureManager { get; }

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
                var structure = ConstructionFactory.CreateFunction(Position, ResourceManager, HexagonManager);
                StructureManager.RemoveStructure(this);
                StructureManager.CreateStrucuture(structure);
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