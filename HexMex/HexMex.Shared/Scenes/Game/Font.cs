using CocosSharp;

namespace HexMex.Scenes.Game
{
    public class Font
    {
        private Font(string fontPath, float fontSize, CCLabelFormat labelFormat)
        {
            FontPath = fontPath;
            FontSize = fontSize;
            FontType = labelFormat;
        }

        public static Font BuildMenuButtonFont { get; } = new Font("fonts/MarkerFelt-22.xnb", 50, CCLabelFormat.SystemFont);
        public static Font MainMenuButtonFont { get; } = new Font("fonts/MarkerFelt-22.xnb", 100, CCLabelFormat.SystemFont);

        public string FontPath { get; }
        public float FontSize { get; }
        public CCLabelFormat FontType { get; }
    }
}