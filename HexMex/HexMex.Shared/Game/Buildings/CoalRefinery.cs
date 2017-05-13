using CocosSharp;

namespace HexMex.Game.Buildings
{
    [BuildingInformation("Coal Refinery", "Converts coal ores into pure coal", 8, new[] {ResourceType.CoalOre}, new[] {ResourceType.Coal}, 1, ResourceType.Iron, ResourceType.Iron, ResourceType.Copper)]
    public class CoalRefinery : Building
    {
        public CoalRefinery(HexagonNode position, World world) : base(position, world, 5, new[] {ResourceType.Degradeable}, new[] {ResourceType.Degradeable})
        {
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, World.WorldSettings.HexagonMargin, ColorCollection.MineBuildingColor);
        }

        protected override bool AllowsRequestingResource(ResourceType resourceType) => true;
    }
}