using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class CopperExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Copper Extractor", "Extracts copper from adjacent hexagons.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Copper, ResourceType.Wood), 7, new StructureDescription.ResourceCollection(ResourceType.CopperOre), new StructureDescription.ResourceCollection(ResourceType.Copper), 2);

        public CopperExtractor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.RedDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.CopperOre });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Copper);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.CopperOre });
        }
    }
}