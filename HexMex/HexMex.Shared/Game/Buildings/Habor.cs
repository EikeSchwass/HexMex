using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Habor : Building
    {
        public static BuildingDescription StructureDescription { get; } = BuildingDescriptionDatabase.Get<Habor>();

        public Habor(HexagonNode position, World world) : base(position, world, StructureDescription)
        {

        }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            drawNode.DrawCircle(position,
                                radius,
                                World.GameSettings.VisualSettings.ColorCollection.BlueNormal,
                                World.GameSettings.VisualSettings.StructureBorderThickness,
                                World.GameSettings.VisualSettings.ColorCollection.White);
        }
    }
}