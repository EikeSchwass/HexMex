using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Forestry : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Forestry", "Needs to be placed adjacent to a forest (duh).", new StructureDescription.ResourceCollection(ResourceType.Iron, ResourceType.Wood, ResourceType.Wood), 3, new StructureDescription.ResourceCollection(ResourceType.Tree), new StructureDescription.ResourceCollection(ResourceType.Wood, ResourceType.Wood, ResourceType.Wood), 2f);

        public Forestry(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GreenNormal,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] {ResourceType.Tree});
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(ResourceType.Wood, ResourceType.Wood, ResourceType.Wood);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] {ResourceType.Tree});
        }
    }
}