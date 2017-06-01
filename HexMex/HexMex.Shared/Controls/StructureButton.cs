using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Helper;

namespace HexMex.Controls
{
    public class StructureButton : Button
    {
        public GameSettings GameSettings { get; }
        public Structure Structure { get; }

        public StructureButton(GameSettings gameSettings, Structure structure)
        {
            GameSettings = gameSettings;
            Structure = structure;
        }

        public override bool IsPointInBounds(CCTouch position)
        {
            var location = ScreenToWorldspace(position.LocationOnScreen);
            var point = this.GetGlobalPosition();
            return (location - point).Length <= GameSettings.VisualSettings.BuildingRadius * 2;
        }
    }
}