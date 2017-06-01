using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class PotaschFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Potasch Factory", "Produces potasch for glas.", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Iron, ResourceType.Copper, ResourceType.Coal, ResourceType.Wood, ResourceType.Wood), 12, new StructureDescription.ResourceCollection(ResourceType.Water, ResourceType.Wood, ResourceType.Wood, ResourceType.Coal), new StructureDescription.ResourceCollection(ResourceType.Pottasche), 5f);

        public PotaschFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayVeryDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Water, ResourceType.Wood, ResourceType.Wood, ResourceType.Coal }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Pottasche);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Water, ResourceType.Wood, ResourceType.Wood, ResourceType.Coal }, null);
        }
    }
}