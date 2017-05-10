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
            BuildMenu = new Menu(new HexButton("Back", worldSettings.HexagonRadius / 4, Font.BuildMenuButtonFont), worldSettings, new MenuEntry("Test0", Font.BuildMenuButtonFont, new MenuEntry("Level1", Font.BuildMenuButtonFont)), new MenuEntry("test2", Font.BuildMenuButtonFont));
            AddChild(BuildMenu);
            BuildMenu.Visible = false;
        }

        public WorldSettings WorldSettings { get; }
        private Menu BuildMenu { get; }

        public void OpenMenu(HexagonNode hexagonNode) => OpenMenu(hexagonNode.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin));

        public void OpenMenu(CCPoint position)
        {
            BuildMenu.Open(position);
        }
    }
}