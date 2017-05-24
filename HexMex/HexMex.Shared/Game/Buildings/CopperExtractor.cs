using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class CopperExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Copper Extractor", "Extracts copper from adjacent hexagons.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Copper, ResourceType.Wood), 7, new StructureDescription.ResourceCollection(ResourceType.CopperOre), new StructureDescription.ResourceCollection(ResourceType.Copper), 2);

        public CopperExtractor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.RedDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.CopperOre });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Copper);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.CopperOre });
        }
    }
}