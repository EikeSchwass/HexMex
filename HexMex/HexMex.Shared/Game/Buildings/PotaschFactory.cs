using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings {
    public class PotaschFactory : Building
    {
        public static BuildingDescription StructureDescription { get; } = BuildingDescriptionDatabase.Get<PotaschFactory>();

        public PotaschFactory(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.GrayVeryDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }
        
        
        
    }
}