using CocosSharp;
using HexMex.Controls;

namespace HexMex.Game.Buildings
{
    public class SolarPowerplant : Building
    {
        public static BuildingDescription StructureDescription { get; } = BuildingDescriptionDatabase.Get<SolarPowerplant>();

        public SolarPowerplant(HexagonNode position, World world) : base(position, world, StructureDescription) { }

        public override void Render(ExtendedDrawNode drawNode, CCPoint position, float radius)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                radius,
                                visualSettings.ColorCollection.BlueLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            CheckAndStartProduction();
        }
    }
}