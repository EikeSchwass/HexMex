using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Laboratory2 : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Laboratory 2", "Generates intermediate knowledge", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Iron, ResourceType.Tools, ResourceType.Copper, ResourceType.Wood), 10, new StructureDescription.ResourceCollection(ResourceType.Tools, ResourceType.Water, ResourceType.Paper), new StructureDescription.ResourceCollection(ResourceType.Knowledge2), 10f);

        public Laboratory2(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

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
            ResourceDirector.RequestIngredients(new[] { ResourceType.Tools, ResourceType.Water, ResourceType.Paper }, null);
        }

        protected override void OnProductionCompleted()
        {
            World.KnowledgeManager.Knowledge2++;
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Tools, ResourceType.Water, ResourceType.Paper }, null);
        }
    }
}