using HexMex.Helper;

namespace HexMex.Game.Buildings
{
    [BuildingInformation("Mine", "Extracts resources from adjacent hexagons", 10, new[] { ResourceType.Minable }, new[] { ResourceType.Minable }, 1, ResourceType.Gold, ResourceType.Gold, ResourceType.Iron)]
    public class MineBuilding : Building
    {

        public MineBuilding(HexagonNode position, World world) : base(position, world, 5, new[] { ResourceType.Any }, new[] { ResourceType.Any })
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