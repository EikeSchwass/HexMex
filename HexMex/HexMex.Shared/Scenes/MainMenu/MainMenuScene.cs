using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;

namespace HexMex.Scenes.MainMenu
{
    public class MainMenuScene : HexMexScene
    {
        public MainMenuScene(CCWindow window, BuildingDescriptionDatabase buildingDescriptionDatabase, ColorCollectionFile colorCollectionFile, LanguageSettings languageSettings) : base(window)
        {
            var mainMenuLayer = new MainMenuLayer(buildingDescriptionDatabase, colorCollectionFile, languageSettings);
            AddChild(mainMenuLayer);
        }
    }
}