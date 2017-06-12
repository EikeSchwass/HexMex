using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class PaperFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<PaperFactory>();

        public PaperFactory(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayVeryLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }
        
        
    }
}