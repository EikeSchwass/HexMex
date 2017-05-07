using HexMex.Helper;

namespace HexMex.Game.Buildings
{
    public class MineBuilding : Structure
    {
        public MineBuilding(HexagonNode position, ResourceManager resourceManager, HexagonManager hexagonManager) : base(position, resourceManager, hexagonManager, new[] { ResourceType.Any }, new[] { ResourceType.Any })
        {

        }

        protected override bool CanExtractResourceFromHexagon(ResourceType resourceType)
        {
            if (resourceType.CanBeUsedFor(ResourceType.Minable))
                return true;
            return false;
        }

        protected override bool AllowsRequestingResource(ResourceType resourceType) => false;

        protected override void StartProduction()
        {
            base.StartProduction();
            OnProductionFinished();
        }
    }
}