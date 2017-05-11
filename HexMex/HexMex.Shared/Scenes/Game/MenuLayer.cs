using CocosSharp;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class MenuLayer : CCLayer
    {
        public MenuLayer(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
            BuildMenu = new BuildMenu(null, WorldSettings,50);
            AddChild(BuildMenu);
            BuildMenu.Visible = false;
        }

        public WorldSettings WorldSettings { get; }
        private BuildMenu BuildMenu { get; }

        public void OpenMenu(HexagonNode hexagonNode) => OpenMenu(hexagonNode.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin));

        public void OpenMenu(CCPoint position)
        {
        }
    }
}