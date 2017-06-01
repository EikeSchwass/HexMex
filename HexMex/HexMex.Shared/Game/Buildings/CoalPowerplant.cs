using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class CoalPowerplant : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Coal Powerplant", "Burns Coal to produce power", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Brick, ResourceType.Wood), 7, new StructureDescription.ResourceCollection(ResourceType.Coal, ResourceType.Coal), new StructureDescription.ResourceCollection(ResourceType.Energy, ResourceType.Energy, ResourceType.Energy, ResourceType.Energy), 1f);

        public CoalPowerplant(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

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
            ResourceDirector.RequestIngredients(new[] { ResourceType.Coal, ResourceType.Coal }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Energy, ResourceType.Energy, ResourceType.Energy, ResourceType.Energy);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Coal, ResourceType.Coal }, null);
        }
    }
}