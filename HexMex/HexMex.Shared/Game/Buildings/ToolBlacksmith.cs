using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class ToolBlacksmith : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<ToolBlacksmith>();

        public ToolBlacksmith(HexagonNode position, World world) : base(position, world, StructureDescription) { }

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