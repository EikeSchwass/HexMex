using CocosSharp;

namespace HexMex.Scenes.MainMenu
{
    public class MainMenuScene : HexMexScene
    {
        public MainMenuScene(CCWindow window) : base(window)
        {
            var mainMenuLayer = new MainMenuLayer();
            AddChild(mainMenuLayer);
        }
    }
}