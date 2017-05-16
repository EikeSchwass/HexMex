using CocosSharp;

namespace HexMex.Game
{
    public class ColorCollection
    {
        // Color table generated via: http://paletton.com/#uid=7370I0kllllaFw0g0qFqFg0w0aF

        public CCColor4B GreenVeryLight => GreenColorPalette[0];
        public CCColor4B GreenLight => GreenColorPalette[1];
        public CCColor4B GreenNormal => GreenColorPalette[2];
        public CCColor4B GreenDark => GreenColorPalette[3];
        public CCColor4B GreenVeryDark => GreenColorPalette[4];

        public CCColor4B BlueVeryLight => BlueColorPalette[0];
        public CCColor4B BlueLight => BlueColorPalette[1];
        public CCColor4B BlueNormal => BlueColorPalette[2];
        public CCColor4B BlueDark => BlueColorPalette[3];
        public CCColor4B BlueVeryDark => BlueColorPalette[4];

        public CCColor4B YellowVeryLight => YellowColorPalette[0];
        public CCColor4B YellowLight => YellowColorPalette[1];
        public CCColor4B YellowNormal => YellowColorPalette[2];
        public CCColor4B YellowDark => YellowColorPalette[3];
        public CCColor4B YellowVeryDark => YellowColorPalette[4];

        public CCColor4B RedVeryLight => RedColorPalette[0];
        public CCColor4B RedLight => RedColorPalette[1];
        public CCColor4B RedNormal => RedColorPalette[2];
        public CCColor4B RedDark => RedColorPalette[3];
        public CCColor4B RedVeryDark => RedColorPalette[4];

        public CCColor4B GrayVeryLight => GrayColorPalette[0];
        public CCColor4B GrayLight => GrayColorPalette[1];
        public CCColor4B GrayNormal => GrayColorPalette[2];
        public CCColor4B GrayDark => GrayColorPalette[3];
        public CCColor4B GrayVeryDark => GrayColorPalette[4];

        public CCColor4B Black => CCColor4B.Black;
        public CCColor4B White => CCColor4B.White;

        protected virtual CCColor4B[] GreenColorPalette { get; } =
        {
            new CCColor4B(113, 170, 151),
            new CCColor4B(71, 141, 118),
            new CCColor4B(38, 113, 88),
            new CCColor4B(14, 85, 62),
            new CCColor4B(0, 57, 38)
        };

        protected virtual CCColor4B[] BlueColorPalette { get; } =
        {
            new CCColor4B(122, 134, 173),
            new CCColor4B(80, 95, 144),
            new CCColor4B(47, 63, 115),
            new CCColor4B(23, 37, 86),
            new CCColor4B(7, 18, 57)
        };

        protected virtual CCColor4B[] YellowColorPalette { get; } =
        {
            new CCColor4B(255, 229, 170),
            new CCColor4B(212, 179, 106),
            new CCColor4B(170, 135, 57),
            new CCColor4B(128, 95, 21),
            new CCColor4B(85, 59, 0)
        };

        protected virtual CCColor4B[] RedColorPalette { get; } =
        {
            new CCColor4B(151, 151, 151),
            new CCColor4B(212, 142, 106),
            new CCColor4B(170, 95, 57),
            new CCColor4B(128, 57, 21),
            new CCColor4B(85, 29, 0)
        };

        protected virtual CCColor4B[] GrayColorPalette { get; } =
        {
            new CCColor4B(151, 151, 151),
            new CCColor4B(117, 117, 117),
            new CCColor4B(88, 88, 88),
            new CCColor4B(61, 61, 61),
            new CCColor4B(38, 38, 38)
        };
    }

    public class DarkerColors : ColorCollection
    {
        protected override CCColor4B[] GreenColorPalette { get; } =
        {
            new CCColor4B(0.035f, 0.541f, 0.067f, 1),
            new CCColor4B(0.224f, 0.702f, 0.255f, 1),
            new CCColor4B(0.086f, 0.655f, 0.125f, 1),
            new CCColor4B(0, 0.447f, 0.027f, 1),
            new CCColor4B(0, 0.329f, 0.024f, 1)
        };

        protected override CCColor4B[] BlueColorPalette { get; } =
        {
            new CCColor4B( 45, 20,120),
            new CCColor4B( 85, 62,156),
            new CCColor4B( 62, 34,145),
            new CCColor4B( 32, 10, 99),
            new CCColor4B( 22,  5, 74)
        };

        protected override CCColor4B[] YellowColorPalette { get; } =
        {
            new CCColor4B(175, 148, 11),
            new CCColor4B(227, 201, 72),
            new CCColor4B(212, 181, 28),
            new CCColor4B(145, 121, 0),
            new CCColor4B(107, 89, 0),
        };

        protected override CCColor4B[] RedColorPalette { get; } =
        {
            new CCColor4B(175, 15, 11),
            new CCColor4B(227, 76, 72),
            new CCColor4B(212, 33, 28),
            new CCColor4B(145,  4,  0),
            new CCColor4B(107,  3,  0),
        };
    }
}