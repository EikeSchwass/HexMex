using CocosSharp;
using HexMex.Helper;

namespace HexMex.Scenes.MainMenu
{
    public class MainMenuScene : HexMexScene
    {
        public MainMenuScene(CCWindow window, DataLoader dataLoader) : base(window, dataLoader)
        {
            var mainMenuLayer = new MainMenuLayer();
            AddChild(mainMenuLayer);
        }
    }
}