using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Scenes.Game;

namespace HexMex.Scenes.Help
{
    public class HelpScene : HexMexScene
    {
        public HelpScene(CCWindow window, VisualSettings visualSettings, LanguageSettings languageSettings) : base(window)
        {
            AddChild(new HelpLayer(visualSettings, languageSettings));
        }
    }

    public class HelpLayer : CCLayer
    {
        public ExtendedDrawNode DrawNode { get; }
        public VisualSettings VisualSettings { get; }
        public LanguageSettings LanguageSettings { get; }
        public string HelpText { get; }
        public HelpLayer(VisualSettings visualSettings, LanguageSettings languageSettings)
        {
            DrawNode = new ExtendedDrawNode();
            VisualSettings = visualSettings;
            LanguageSettings = languageSettings;
            HelpText = languageSettings.GetByKey(new TranslationKey("helpText"));
            AddChild(DrawNode);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            DrawNode.Clear();
            DrawNode.DrawText(VisibleBoundsWorldspace.Center, HelpText, Font.ArialFonts[VisualSettings.HelpTextFontSize], VisibleBoundsWorldspace.Size * 0.8f);
        }
    }
}