using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class PaperFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Paper Factory", "Produces paper from wood.", new StructureDescription.ResourceCollection(ResourceType.Copper, ResourceType.Iron, ResourceType.Tools), 3, new StructureDescription.ResourceCollection(ResourceType.Wood, ResourceType.Wood, ResourceType.Water), new StructureDescription.ResourceCollection(ResourceType.Paper), 3f);

        public PaperFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

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
            ResourceDirector.RequestIngredients(new[] { ResourceType.Wood, ResourceType.Wood, ResourceType.Water }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Paper);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Wood, ResourceType.Wood, ResourceType.Water }, null);
        }
    }
}