using System.Linq;
using CocosSharp;
using HexMex.Helper;
using static HexMex.Game.Buildings.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class DiamondExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Diamond Extractor", "Extracts diamonds from adjacent hexagons. Diamonds can be used for everything, except pure water.", new ResourceCollection(Tools, Circuit, Circuit, Gold, Copper, Glas), 20, new ResourceCollection(DiamondOre), new ResourceCollection(Diamond), 5);

        private ResourceHexagon[] AdjacentDaimondHexagons { get; set; }

        public DiamondExtractor(HexagonNode position, World world) : base(position, world)
        {
            AdjacentDaimondHexagons = World.HexagonManager.GetAdjacentHexagons(Position).OfType<ResourceHexagon>().Where(h => h.ResourceType == DiamondOre).ToArray();
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            drawNode.DrawCircle(position, World.GameSettings.LayoutSettings.HexagonMargin * 2, World.GameSettings.VisualSettings.ColorCollection.YellowLight, World.GameSettings.VisualSettings.StructureBorderThickness, World.GameSettings.VisualSettings.ColorCollection.White);
        }

        protected override void Idling()
        {
            base.Idling();
            if (AdjacentDaimondHexagons.Length == 0)
                return;
            var hexagons = AdjacentDaimondHexagons.Where(h => h.RemainingResources > 0).ToArray();
            if (hexagons.Length != AdjacentDaimondHexagons.Length)
                AdjacentDaimondHexagons = hexagons;
            if (AdjacentDaimondHexagons.Length == 0)
                return;
#if !DEBUG
            var index = HexMexRandom.Next(hexagons.Length);
            AdjacentDaimondHexagons[index].RemainingResources--;
#endif
            StartProduction(StructureDescription.ProductionInformation.ProductionTime);
        }

        protected override void OnProductionCompleted()
        {
            base.OnProductionCompleted();
            ResourceDirector.ProvideResources(Diamond);
        }
    }
}