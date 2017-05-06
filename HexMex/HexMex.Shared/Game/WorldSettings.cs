using Java.Lang;

namespace HexMex.Game
{
    public class WorldSettings
    {
        public float HexagonRadius => 180;
        public int MaxNumberOfResourcesInHexagon => 294;
        public float ResourceTriangleHeight { get; }
        public float ResourceTriangleEdgeLength { get; }
        public float HexagonMargin => 4;

        public WorldSettings()
        {
            ResourceTriangleEdgeLength = (float)(HexagonRadius / Math.Sqrt(MaxNumberOfResourcesInHexagon / 6f));
            ResourceTriangleHeight = (float)(Math.Sqrt(3) / 2 * ResourceTriangleEdgeLength);
        }
    }
}