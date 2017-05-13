using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game.Buildings
{
    [BuildingInformation("Mine", "Extracts resources from adjacent degradeable hexagons", 10, new[] {ResourceType.Degradeable}, new[] {ResourceType.Degradeable}, 1, ResourceType.Iron, ResourceType.Iron, ResourceType.Copper)]
    public class MineBuilding : Building
    {
        public MineBuilding(HexagonNode position, World world) : base(position, world, 5, new[] {ResourceType.Degradeable}, new[] {ResourceType.Degradeable})
        {
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, World.WorldSettings.HexagonMargin, ColorCollection.MineBuildingColor);
        }

        protected override bool AllowsRequestingResource(ResourceType resourceType) => false;

        protected override bool CanExtractResourceFromHexagon(ResourceType resourceType)
        {
            if (resourceType.CanBeUsedFor(ResourceType.Degradeable))
                return true;
            return false;
        }
    }
}