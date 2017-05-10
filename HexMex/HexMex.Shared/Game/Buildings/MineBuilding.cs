using HexMex.Helper;

namespace HexMex.Game.Buildings
{
    public class MineBuilding : Structure
    {
        private const float PRODUCTION_TIME = 5f;
        private float CurrentProductionTime { get; set; }
        private bool Producing { get; set; }

        public MineBuilding(HexagonNode position, ResourceManager resourceManager, HexagonManager hexagonManager) : base(position, resourceManager, hexagonManager, new[] { ResourceType.Any }, new[] { ResourceType.Any })
        {

        }

        protected override bool CanExtractResourceFromHexagon(ResourceType resourceType)
        {
            if (resourceType.CanBeUsedFor(ResourceType.Minable))
                return true;
            return false;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Producing)
                return;
            CurrentProductionTime += dt;
            if (CurrentProductionTime >= PRODUCTION_TIME)
            {
                OnProductionFinished();
                CurrentProductionTime = 0;
                Producing = false;
            }
        }

        protected override bool AllowsRequestingResource(ResourceType resourceType) => false;

        protected override void StartProduction()
        {
            base.StartProduction();
            Producing = true;
        }
    }
}