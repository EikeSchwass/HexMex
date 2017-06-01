using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class WaterPowerplant : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Water Powerplant", "Needs adjacent water to produce power", new StructureDescription.ResourceCollection(ResourceType.Brick, ResourceType.Brick, ResourceType.Iron, ResourceType.Wood), 12, new StructureDescription.ResourceCollection(ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater), new StructureDescription.ResourceCollection(ResourceType.Energy, ResourceType.Energy), 1f);

        public WaterPowerplant(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.BlueLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Energy, ResourceType.Energy);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater, ResourceType.PureWater });
        }
    }
}