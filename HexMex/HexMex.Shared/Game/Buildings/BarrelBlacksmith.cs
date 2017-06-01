using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class BarrelBlacksmith : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Barrel Blacksmith", "Produces barrels that can be used to transport water.", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Iron, ResourceType.Wood), 5, new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Wood, ResourceType.Wood), new StructureDescription.ResourceCollection(ResourceType.Barrel), 4f);

        public BarrelBlacksmith(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime,StructureDescription) { }

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
            ResourceDirector.RequestIngredients(new[] { ResourceType.Iron, ResourceType.Iron, ResourceType.Wood }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Barrel);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { ResourceType.Iron, ResourceType.Iron, ResourceType.Wood }, null);
        }
    }
}