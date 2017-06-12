using System.Linq;
using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Forestry : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<Forestry>();

        public Forestry(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GreenNormal,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void RequestIngredients()
        {
            ResourceDirector.RequestIngredients(null, StructureDescription.ProductionInformation.Ingredients.ResourceTypes.ToArray());
        }
        
        
    }
}