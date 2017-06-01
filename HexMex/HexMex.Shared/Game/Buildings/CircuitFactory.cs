using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class CircuitFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Circuit Factory", "Produces circuits.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Copper, ResourceType.Copper, ResourceType.Tools), 7, new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Gold, ResourceType.Copper, ResourceType.Copper), new StructureDescription.ResourceCollection(ResourceType.Circuit), 9f);

        public CircuitFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GreenLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Iron, ResourceType.Gold, ResourceType.Copper, ResourceType.Copper }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Barrel);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Iron, ResourceType.Gold, ResourceType.Copper, ResourceType.Copper }, null);
        }
    }
}