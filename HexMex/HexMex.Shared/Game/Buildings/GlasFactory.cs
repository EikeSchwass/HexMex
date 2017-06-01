using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class GlasFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Glas Factory", "Produces glas from potash and sand.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Iron, ResourceType.Brick, ResourceType.Circuit, ResourceType.Coal, ResourceType.Coal, ResourceType.Copper, ResourceType.Wood), 12, new StructureDescription.ResourceCollection(ResourceType.Pottasche, ResourceType.Pottasche, ResourceType.Pottasche, ResourceType.Pottasche, ResourceType.Sand, ResourceType.Sand, ResourceType.Sand, ResourceType.Sand, ResourceType.Sand), new StructureDescription.ResourceCollection(ResourceType.Glas), 14f);

        public GlasFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

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