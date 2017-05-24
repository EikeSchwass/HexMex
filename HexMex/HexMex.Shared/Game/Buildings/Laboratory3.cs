using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class Laboratory3 : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Laboratory 3", "Generates advanced knowledge", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Iron, ResourceType.Tools, ResourceType.Tools, ResourceType.Copper, ResourceType.Circuit, ResourceType.Circuit), 15, new StructureDescription.ResourceCollection(ResourceType.Knowledge1, ResourceType.Knowledge1, ResourceType.Knowledge2, ResourceType.Tools, ResourceType.Water, ResourceType.Paper, ResourceType.Circuit), new StructureDescription.ResourceCollection(ResourceType.Knowledge3), 20f);

        public Laboratory3(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.White,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Knowledge1, ResourceType.Knowledge1, ResourceType.Knowledge2, ResourceType.Tools, ResourceType.Water, ResourceType.Paper, ResourceType.Circuit }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Knowledge3);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Knowledge1, ResourceType.Knowledge1, ResourceType.Knowledge2, ResourceType.Tools, ResourceType.Water, ResourceType.Paper, ResourceType.Circuit }, null);
        }
    }
}