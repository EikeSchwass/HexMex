using System;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Scenes.Game;

namespace HexMex.Scenes.Help
{
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

            var blocks = HelpText.Split(new[] { "$" }, StringSplitOptions.RemoveEmptyEntries);

            var rects = new CCRect[blocks.Length];
            float margin = VisibleBoundsWorldspace.Size.Width / 1080 * 10;
            float width = VisibleBoundsWorldspace.Size.Width - margin * 2;
            float height = VisibleBoundsWorldspace.Size.Height / blocks.Length;


            for (int i = 0; i < blocks.Length; i++)
            {
                float x = VisibleBoundsWorldspace.MinX + margin;
                float y = VisibleBoundsWorldspace.MaxY - margin - height * (i + 1);
                rects[i] = new CCRect(x, y, width, height);
            }

            for (int i = 0; i < rects.Length; i++)
            {
                DrawNode.DrawText(rects[i].Center, blocks[i].Replace("$", ""), Font.ArialFonts[VisualSettings.HelpTextFontSize], rects[i].Size * 0.95f);
            }

        }
    }
}