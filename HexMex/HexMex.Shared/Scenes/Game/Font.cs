using System.Collections.Generic;
using System.Collections.ObjectModel;
using CocosSharp;

namespace HexMex.Scenes.Game
{
    public class Font
    {
        private readonly int fontSize;
        public static IReadOnlyDictionary<int, Font> ArialFonts { get; } = new ReadOnlyDictionary<int, Font>(new Dictionary<int, Font>
        {
            {5, new Font("fonts/arial", 5, CCLabelFormat.SpriteFont)},
            {6, new Font("fonts/arial", 6, CCLabelFormat.SpriteFont)},
            {7, new Font("fonts/arial", 7, CCLabelFormat.SpriteFont)},
            {8, new Font("fonts/arial", 8, CCLabelFormat.SpriteFont)},
            {9, new Font("fonts/arial", 9, CCLabelFormat.SpriteFont)},
            {10, new Font("fonts/arial", 10, CCLabelFormat.SpriteFont)},
            {11, new Font("fonts/arial", 11, CCLabelFormat.SpriteFont)},
            {12, new Font("fonts/arial", 12, CCLabelFormat.SpriteFont)},
            {13, new Font("fonts/arial", 13, CCLabelFormat.SpriteFont)},
            {14, new Font("fonts/arial", 14, CCLabelFormat.SpriteFont)},
            {15, new Font("fonts/arial", 15, CCLabelFormat.SpriteFont)},
            {16, new Font("fonts/arial", 16, CCLabelFormat.SpriteFont)},
            {17, new Font("fonts/arial", 17, CCLabelFormat.SpriteFont)},
            {18, new Font("fonts/arial", 18, CCLabelFormat.SpriteFont)},
            {19, new Font("fonts/arial", 19, CCLabelFormat.SpriteFont)},
            {20, new Font("fonts/arial", 20, CCLabelFormat.SpriteFont)},
            {21, new Font("fonts/arial", 21, CCLabelFormat.SpriteFont)},
            {22, new Font("fonts/arial", 22, CCLabelFormat.SpriteFont)},
            {23, new Font("fonts/arial", 23, CCLabelFormat.SpriteFont)},
            {24, new Font("fonts/arial", 24, CCLabelFormat.SpriteFont)},
            {25, new Font("fonts/arial", 25, CCLabelFormat.SpriteFont)},
            {26, new Font("fonts/arial", 26, CCLabelFormat.SpriteFont)},
            {27, new Font("fonts/arial", 27, CCLabelFormat.SpriteFont)},
            {28, new Font("fonts/arial", 28, CCLabelFormat.SpriteFont)},
            {29, new Font("fonts/arial", 29, CCLabelFormat.SpriteFont)},
            {30, new Font("fonts/arial", 30, CCLabelFormat.SpriteFont)},
            {31, new Font("fonts/arial", 31, CCLabelFormat.SpriteFont)},
            {32, new Font("fonts/arial", 32, CCLabelFormat.SpriteFont)},
            {33, new Font("fonts/arial", 33, CCLabelFormat.SpriteFont)},
            {34, new Font("fonts/arial", 34, CCLabelFormat.SpriteFont)},
            {35, new Font("fonts/arial", 35, CCLabelFormat.SpriteFont)},
            {36, new Font("fonts/arial", 36, CCLabelFormat.SpriteFont)},
            {37, new Font("fonts/arial", 37, CCLabelFormat.SpriteFont)},
            {38, new Font("fonts/arial", 38, CCLabelFormat.SpriteFont)},
            {39, new Font("fonts/arial", 39, CCLabelFormat.SpriteFont)},
            {40, new Font("fonts/arial", 40, CCLabelFormat.SpriteFont)},
            {41, new Font("fonts/arial", 41, CCLabelFormat.SpriteFont)},
            {42, new Font("fonts/arial", 42, CCLabelFormat.SpriteFont)},
            {43, new Font("fonts/arial", 43, CCLabelFormat.SpriteFont)},
            {44, new Font("fonts/arial", 44, CCLabelFormat.SpriteFont)},
            {45, new Font("fonts/arial", 45, CCLabelFormat.SpriteFont)},
            {46, new Font("fonts/arial", 46, CCLabelFormat.SpriteFont)},
            {47, new Font("fonts/arial", 47, CCLabelFormat.SpriteFont)},
            {48, new Font("fonts/arial", 48, CCLabelFormat.SpriteFont)},
            {49, new Font("fonts/arial", 49, CCLabelFormat.SpriteFont)},
            {50, new Font("fonts/arial", 50, CCLabelFormat.SpriteFont)},
            {51, new Font("fonts/arial", 51, CCLabelFormat.SpriteFont)},
            {52, new Font("fonts/arial", 52, CCLabelFormat.SpriteFont)},
            {53, new Font("fonts/arial", 53, CCLabelFormat.SpriteFont)},
            {54, new Font("fonts/arial", 54, CCLabelFormat.SpriteFont)},
            {55, new Font("fonts/arial", 55, CCLabelFormat.SpriteFont)},
            {56, new Font("fonts/arial", 56, CCLabelFormat.SpriteFont)},
            {57, new Font("fonts/arial", 57, CCLabelFormat.SpriteFont)},
            {58, new Font("fonts/arial", 58, CCLabelFormat.SpriteFont)},
            {59, new Font("fonts/arial", 59, CCLabelFormat.SpriteFont)},
            {60, new Font("fonts/arial", 60, CCLabelFormat.SpriteFont)},
            {61, new Font("fonts/arial", 61, CCLabelFormat.SpriteFont)},
            {62, new Font("fonts/arial", 62, CCLabelFormat.SpriteFont)},
            {63, new Font("fonts/arial", 63, CCLabelFormat.SpriteFont)},
            {64, new Font("fonts/arial", 64, CCLabelFormat.SpriteFont)}
        });

        public static float FontScaleFactor { get; set; } = 1;

        public string FontPath { get; }
        public int FontSize => (int)(fontSize * FontScaleFactor);
        public CCLabelFormat FontType { get; }

        private Font(string fontPath, int fontSize, CCLabelFormat labelFormat)
        {
            FontPath = fontPath;
            this.fontSize = fontSize;
            FontType = labelFormat;
        }
    }
}