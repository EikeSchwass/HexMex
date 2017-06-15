using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class Laboratory1 : Building
    {
        public static BuildingDescription StructureDescription { get; } = BuildingDescriptionDatabase.Get<Laboratory1>();

        public Laboratory1(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }


        protected override void OnProductionCompleted()
        {
            World.GlobalResourceManager.Knowledge += Knowledge.Knowledge1One;
        }
    }
}