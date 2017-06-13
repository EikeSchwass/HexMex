using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Laboratory2 : Building
    {
        public static StructureDescription StructureDescription { get; } = StructureDescriptionDatabase.Get<Laboratory2>();

        public Laboratory2(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayVeryLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnProductionCompleted()
        {
            World.GlobalResourceManager.Knowledge += Knowledge.Knowledge2One;
        }
    }
}