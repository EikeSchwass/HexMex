using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class SandFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Sand Factory", "Produces sand from stone. Needs to be adjacent to stone", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Tools, ResourceType.Copper, ResourceType.Wood), 10, new StructureDescription.ResourceCollection(ResourceType.Stone, ResourceType.Stone), new StructureDescription.ResourceCollection(ResourceType.Sand, ResourceType.Sand, ResourceType.Sand), 7f);

        public SandFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.YellowDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.Stone, ResourceType.Stone });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Sand, ResourceType.Sand, ResourceType.Sand);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.Stone, ResourceType.Stone });
        }
    }
}