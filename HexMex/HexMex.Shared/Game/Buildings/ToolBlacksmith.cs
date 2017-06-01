using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class ToolBlacksmith : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Tool Blacksmith", "Needs Iron and produces tools.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Iron, ResourceType.Iron), 6, new StructureDescription.ResourceCollection(ResourceType.Iron), new StructureDescription.ResourceCollection(ResourceType.Tools), 3f);

        public ToolBlacksmith(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayVeryLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] {ResourceType.Iron}, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Tools);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] {ResourceType.Iron}, null);
        }
    }
}