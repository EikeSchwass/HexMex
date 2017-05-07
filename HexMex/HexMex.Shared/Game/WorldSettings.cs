using System;

namespace HexMex.Game
{
    public class WorldSettings
    {
        public WorldSettings()
        {
            var layerCount = Math.Sqrt(MaxNumberOfResourcesInHexagon / 6f);
            ResourceTriangleEdgeLength = (float)(HexagonRadius / layerCount - 2 / layerCount);
            ResourceTriangleHeight = (float)(Math.Sqrt(3) / 2 * ResourceTriangleEdgeLength);
            UniversalResourceStartFactor = 1;
            HexagonMargin = 16;
        }

        public float HexagonMargin { get; }
        public float HexagonRadius => 280;
        public int MaxNumberOfResourcesInHexagon => 294;
        public float ResourceManagerUpdateInterval { get; } = 1;
        public float ResourceTimeBetweenNodes => 1;
        public float ResourceTriangleEdgeLength { get; }
        public float ResourceTriangleHeight { get; }
        public float UniversalResourceStartFactor { get; }
        public float MapSizeFactor { get; } = 1;
    }
}