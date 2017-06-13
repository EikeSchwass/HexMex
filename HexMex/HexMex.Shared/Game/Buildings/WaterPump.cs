using CocosSharp;
using HexMex.Controls;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class WaterPump : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<WaterPump>();

        public WaterPump(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.BlueDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }
    }
}