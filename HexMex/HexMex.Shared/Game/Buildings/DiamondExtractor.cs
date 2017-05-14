using System.Linq;
using CocosSharp;
using static HexMex.Game.Buildings.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class DiamondExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Diamond Extractor", "Extracts diamonds from adjacent hexagons. Diamonds can be used for everything, except pure water.", new ResourceCollection(Tools, Circuit, Circuit, Gold, Copper, Glas), 60, new ResourceCollection(DiamondOre), new ResourceCollection(Diamond), 5);

        private ResourceHexagon[] AdjacentDaimongHexagons { get; set; }

        public DiamondExtractor(HexagonNode position, World world) : base(position, world)
        {
            AdjacentDaimongHexagons = World.HexagonManager.GetAdjacentHexagons(Position).OfType<ResourceHexagon>().Where(h => h.ResourceType == DiamondOre).ToArray();
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, World.WorldSettings.HexagonMargin * 2, ColorCollection.MineBuildingColor);
        }

        protected override void Idling()
        {
            base.Idling();
            if (AdjacentDaimongHexagons.Length == 0)
                return;
            var hexagons = AdjacentDaimongHexagons.Where(h => h.RemainingResources > 0).ToArray();
            if (hexagons.Length != AdjacentDaimongHexagons.Length)
                AdjacentDaimongHexagons = hexagons;
            if (AdjacentDaimongHexagons.Length == 0)
                return;
            var index = HexMexRandom.Next(hexagons.Length);
            AdjacentDaimongHexagons[index].RemainingResources--;
            StartProduction(StructureDescription.ProductionInformation.ProductionTime);
        }

        protected override void OnProductionCompleted()
        {
            base.OnProductionCompleted();
            ResourceDirector.ProvideResources(Diamond);
        }
    }
}