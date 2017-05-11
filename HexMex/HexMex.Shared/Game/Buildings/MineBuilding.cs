using HexMex.Helper;

namespace HexMex.Game.Buildings
{
    [BuildingInformation("Mine", 10, ResourceType.Gold, ResourceType.Gold, ResourceType.Iron)]
    public class MineBuilding : Building
    {

        public MineBuilding(HexagonNode position, ResourceManager resourceManager, HexagonManager hexagonManager) : base(position, resourceManager, hexagonManager, 5, new[] { ResourceType.Any }, new[] { ResourceType.Any })
        {

        }

        protected override bool CanExtractResourceFromHexagon(ResourceType resourceType)
        {
            if (resourceType.CanBeUsedFor(ResourceType.Minable))
                return true;
            return false;
        }

        protected override bool AllowsRequestingResource(ResourceType resourceType) => false;
    }
}