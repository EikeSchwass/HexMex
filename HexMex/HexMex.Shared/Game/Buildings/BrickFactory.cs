using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class BrickFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Brick Factory", "Produces bricks from stone. Needs to be adjacent to stone", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Copper, ResourceType.Wood), 8, new StructureDescription.ResourceCollection(ResourceType.Stone, ResourceType.Stone, ResourceType.Water), new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Brick), 8f);

        public BrickFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.RedLight,
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