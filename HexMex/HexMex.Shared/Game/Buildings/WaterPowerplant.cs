using System.Linq;
using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class WaterPowerplant : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<WaterPowerplant>();

        public WaterPowerplant(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.BlueLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }
    }
}