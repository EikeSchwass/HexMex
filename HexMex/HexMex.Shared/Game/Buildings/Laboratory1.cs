using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Laboratory1 : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Laboratory 1", "Generates simple knowledge", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Iron, ResourceType.Copper, ResourceType.Wood), 7, new StructureDescription.ResourceCollection(ResourceType.Water, ResourceType.Coal, ResourceType.Wood), new StructureDescription.ResourceCollection(ResourceType.Knowledge1), 5f);

        public Laboratory1(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Water, ResourceType.Coal, ResourceType.Wood }, null);
        }

        protected override void OnProductionCompleted()
        {
            World.KnowledgeManager.Knowledge1++;
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Water, ResourceType.Coal, ResourceType.Wood }, null);
        }
    }
}