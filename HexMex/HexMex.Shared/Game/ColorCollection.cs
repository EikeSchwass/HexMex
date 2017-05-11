using CocosSharp;

namespace HexMex.Game
{
    public static class ColorCollection
    {
        public static CCColor4B DefaultHexagonBackgroundColor { get; } = new CCColor4B(30, 30, 35, 255);
        public static CCColor4B DefaultHexagonBorderColor { get; } = CCColor4B.White; //new CCColor4B(5, 103, 242, 255);
        public static CCColor4B DefaultResourceBorderColor { get; } = CCColor4B.White;
        public static CCColor4B DefaultStructureBackgroundColor { get; } = CCColor4B.Gray;
        public static CCColor4B DefaultStructureBorderColor { get; } = CCColor4B.White;
        public static CCColor4B DefaultBuildButtonBackgroundColor { get; } = CCColor4B.Gray;
        public static CCColor4B DefaultBuildButtonBorderColor { get; } = CCColor4B.White;
        public static CCColor4B DefaultConstructionBackgroundColor { get; } = CCColor4B.Gray;
        public static CCColor4B DefaultConstructionProgressColor { get; } = CCColor4B.Lerp(CCColor4B.Gray, CCColor4B.Black, 0.5f);
    }
}
