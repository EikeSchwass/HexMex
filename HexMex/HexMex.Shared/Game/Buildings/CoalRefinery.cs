using CocosSharp;
using static HexMex.Game.Buildings.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class CoalRefinery : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Coal Refinery", "Converts coal ore to coal", new ResourceCollection(Iron, Copper), 15, new ResourceCollection(CoalOre), new ResourceCollection(Coal), 5);

        public CoalRefinery(HexagonNode position, World world) : base(position, world)
        {
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, World.WorldSettings.HexagonMargin, ColorCollection.MineBuildingColor);
        }
    }
}