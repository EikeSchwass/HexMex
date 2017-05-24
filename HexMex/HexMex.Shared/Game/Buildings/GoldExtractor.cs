using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class GoldExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Gold Extractor", "Extracts gold from adjacent hexagons.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Iron, ResourceType.Stone, ResourceType.Copper, ResourceType.Wood), 7, new StructureDescription.ResourceCollection(ResourceType.GoldOre), new StructureDescription.ResourceCollection(ResourceType.Gold), 3.5f);

        public GoldExtractor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.YellowLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.GoldOre });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Gold);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.GoldOre });
        }
    }
}