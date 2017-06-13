using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Laboratory3 : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<Laboratory3>();

        public Laboratory3(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.White,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }


        protected override void OnProductionCompleted()
        {
            World.GlobalResourceManager.Knowledge += Knowledge.Knowledge3One;
        }

    }
}