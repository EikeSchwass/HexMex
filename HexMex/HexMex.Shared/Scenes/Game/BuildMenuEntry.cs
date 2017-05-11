using CocosSharp;
using HexMex.Controls;
using HexMex.Game.Buildings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class BuildMenuEntry : Control
    {
        private BuildingConstructionFactory Factory { get; }

        public BuildMenuEntry(BuildingConstructionFactory factory)
        {
            Factory = factory;

            var label = new CCLabel(factory.BuildingInformation.Name, Font.BuildMenuButtonFont.FontPath, Font.BuildMenuButtonFont.FontSize);
            AddChild(label);
        }

        public override bool IsPointInBounds(CCTouch position)
        {
            var location = ScreenToWorldspace(position.LocationOnScreen);
            var globalPosition = this.GetGlobalPosition();
            return (location - globalPosition).Length <= 60;
        }
    }
}