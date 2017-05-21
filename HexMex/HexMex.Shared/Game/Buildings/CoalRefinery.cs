using HexMex.Controls;
using static HexMex.Game.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class CoalRefinery : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Coal Refinery", "Converts coal ore to coal", new ResourceCollection(Coal, Coal, Iron), 15, new ResourceCollection(CoalOre), new ResourceCollection(Coal), 5);

        public CoalRefinery(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime)
        {
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { CoalOre });
        }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            drawNode.DrawCircle(position, World.GameSettings.VisualSettings.BuildingRadius, World.GameSettings.VisualSettings.ColorCollection.GrayDark, World.GameSettings.VisualSettings.StructureBorderThickness, World.GameSettings.VisualSettings.ColorCollection.White);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Coal);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { CoalOre });
        }
    }
}