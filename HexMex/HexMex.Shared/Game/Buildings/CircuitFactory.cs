using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class CircuitFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<CircuitFactory>();

        public CircuitFactory(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GreenLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }
    }
}