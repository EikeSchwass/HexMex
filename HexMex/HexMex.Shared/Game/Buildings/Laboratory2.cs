using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class Laboratory2 : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Laboratory 2", "Generates intermediate knowledge", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Iron, ResourceType.Tools, ResourceType.Copper, ResourceType.Wood), 10, new StructureDescription.ResourceCollection(ResourceType.Knowledge1, ResourceType.Tools, ResourceType.Water, ResourceType.Paper), new StructureDescription.ResourceCollection(ResourceType.Knowledge2), 10f);

        public Laboratory2(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.GrayVeryLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Knowledge1, ResourceType.Tools, ResourceType.Water, ResourceType.Paper }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Knowledge2);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Knowledge1, ResourceType.Tools, ResourceType.Water, ResourceType.Paper }, null);
        }
    }
}