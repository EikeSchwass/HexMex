using CocosSharp;
using HexMex.Controls;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class WaterPump : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Water Pump", "Pumps water from an adjacent hexagon and fills it into barrels for transportation.", new StructureDescription.ResourceCollection(Brick, Copper, Tools, Circuit), 7, new StructureDescription.ResourceCollection(Barrel, PureWater, PureWater), new StructureDescription.ResourceCollection(WaterBarrel), 4f);

        public WaterPump(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.BlueDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { Barrel }, new[] { PureWater, PureWater });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(WaterBarrel);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { Barrel }, new[] { PureWater, PureWater });
        }
    }
}