using CocosSharp;

namespace HexMex.Game
{
    public static class ColorCollection
    {
        public static CCColor4B DefaultHexagonBacgroundColor { get; } = new CCColor4B(30, 30, 35, 255);
        public static CCColor4B DefaultHexagonBorderColor { get; } = CCColor4B.White; //new CCColor4B(5, 103, 242, 255);
        public static CCColor4B DefaultResourceBorderColor { get; } = CCColor4B.White; //new CCColor4B(5, 103, 242, 255);
    }
}
