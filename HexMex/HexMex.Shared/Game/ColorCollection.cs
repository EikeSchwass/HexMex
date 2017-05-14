using CocosSharp;

namespace HexMex.Game
{
    public static class ColorCollection
    {
        public static CCColor4B BuildButtonBackgroundColor { get; } = CCColor4B.Gray;
        public static CCColor4B BuildButtonBorderColor { get; } = CCColor4B.White;
        public static CCColor4B BuildMenuBackgroundColor { get; } = new CCColor4B(0f, 0f, 0f, 0.75f);
        public static CCColor4B ConstructionBackgroundColor { get; } = CCColor4B.Gray;
        public static CCColor4B ConstructionProgressColor { get; } = CCColor4B.Lerp(CCColor4B.Gray, CCColor4B.Black, 0.5f);
        public static CCColor4B HexagonBackgroundColor { get; } = new CCColor4B(30, 30, 35, 255);
        public static CCColor4B HexagonBorderColor { get; } = CCColor4B.White; //new CCColor4B(5, 103, 242, 255);
        public static CCColor4B MineBuildingColor { get; } = new CCColor4B(139, 69, 19);
        public static CCColor4B ResourceBorderColor { get; } = CCColor4B.White;
        public static CCColor4B StructureBackgroundColor { get; } = CCColor4B.Gray;
        public static CCColor4B StructureBorderColor { get; } = CCColor4B.White;
        public static CCColor4B VillageBuildingColor { get; } = new CCColor4B(218, 165, 32);
    }
}