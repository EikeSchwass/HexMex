using System.Linq;
using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class CoalRefinery : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<CoalRefinery>();

        public CoalRefinery(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            drawNode.DrawCircle(position,
                                radius,
                                World.GameSettings.VisualSettings.ColorCollection.GrayDark,
                                World.GameSettings.VisualSettings.StructureBorderThickness,
                                World.GameSettings.VisualSettings.ColorCollection.White);
        }

        protected override void RequestIngredients()
        {
            ResourceDirector.RequestIngredients(null, StructureDescription.ProductionInformation.Ingredients.ResourceTypes.ToArray());
        }
        
        
    }
}