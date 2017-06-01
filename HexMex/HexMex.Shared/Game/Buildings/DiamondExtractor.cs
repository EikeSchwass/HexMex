using CocosSharp;
using HexMex.Controls;
using static HexMex.Game.StructureDescription;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class DiamondExtractor : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Diamond Extractor", "Extracts diamonds from adjacent hexagons. Diamonds can be used to trade at a habor.", new ResourceCollection(Tools, Circuit, Circuit, Gold, Copper, Glas), 20, new ResourceCollection(DiamondOre), new ResourceCollection(Diamond), 3);

        public DiamondExtractor(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.YellowLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { DiamondOre });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Diamond);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { DiamondOre });
        }
    }
}