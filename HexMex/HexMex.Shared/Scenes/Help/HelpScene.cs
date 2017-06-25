using CocosSharp;
using HexMex.Game.Settings;

namespace HexMex.Scenes.Help
{
    public class HelpScene : HexMexScene
    {
        public HelpScene(CCWindow window, VisualSettings visualSettings, LanguageSettings languageSettings) : base(window)
        {
            AddChild(new HelpLayer(visualSettings, languageSettings));
        }
    }
}