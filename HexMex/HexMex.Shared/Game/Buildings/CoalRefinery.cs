using CocosSharp;
using HexMex.Controls;
using static HexMex.Game.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class CoalRefinery : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Coal Refinery", "Converts coal ore to coal", new ResourceCollection(Coal, Coal, Iron), 15, new ResourceCollection(CoalOre), new ResourceCollection(Coal), 5);

        public CoalRefinery(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            drawNode.DrawCircle(position,
                                radius,
                                World.GameSettings.VisualSettings.ColorCollection.GrayDark,
                                World.GameSettings.VisualSettings.StructureBorderThickness,
                                World.GameSettings.VisualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] {CoalOre});
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Coal);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] {CoalOre});
        }
    }
}