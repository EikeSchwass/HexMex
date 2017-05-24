using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class IronExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Iron Extractor", "Extracts iron from adjacent hexagons.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Iron, ResourceType.Wood), 8, new StructureDescription.ResourceCollection(ResourceType.IronOre), new StructureDescription.ResourceCollection(ResourceType.Iron), 2);

        public IronExtractor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.GrayLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.IronOre });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Iron);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.IronOre });
        }
    }
}