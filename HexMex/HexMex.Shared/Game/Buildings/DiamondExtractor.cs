using CocosSharp;
using HexMex.Helper;
using static HexMex.Game.Buildings.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class DiamondExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Diamond Extractor", "Extracts diamonds from adjacent hexagons. Diamonds can be used for everything, except pure water.", new ResourceCollection(Tools, Circuit, Circuit, Gold, Copper, Glas), 20, new ResourceCollection(DiamondOre), new ResourceCollection(Diamond), 2);

        public DiamondExtractor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime)
        {
        }

        public override void Render(CCDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            drawNode.DrawCircle(position, World.GameSettings.VisualSettings.BuildingRadius, World.GameSettings.VisualSettings.ColorCollection.YellowLight, World.GameSettings.VisualSettings.StructureBorderThickness, World.GameSettings.VisualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            base.OnAddedToWorld();
            ResourceDirector.RequestIngredients(null, new[] { DiamondOre });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Diamond);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { DiamondOre });
        }
    }
}