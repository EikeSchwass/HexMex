using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Habor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Habor", "Must be placed adjacent to water. Trades diamonds for needed resources.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Wood, ResourceType.Wood, ResourceType.Copper), 10, new StructureDescription.ResourceCollection(ResourceType.Diamond), new StructureDescription.ResourceCollection(ResourceType.Anything), 2.5f);

        public Habor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            drawNode.DrawCircle(position,
                                World.GameSettings.VisualSettings.BuildingRadius,
                                World.GameSettings.VisualSettings.ColorCollection.BlueNormal,
                                World.GameSettings.VisualSettings.StructureBorderThickness,
                                World.GameSettings.VisualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Diamond }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Anything);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Diamond }, null);
        }
    }
}