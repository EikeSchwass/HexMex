using System;
using CocosSharp;

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
            HexagonMargin = 32;
        }

        public float HexagonMargin { get; }
        public float HexagonRadius => 280;
        public int MaxNumberOfResourcesInHexagon => 294;
        public float ResourceManagerUpdateInterval { get; } = 1;
        public float ResourceTimeBetweenNodes => 1f;
        public float ResourceTriangleEdgeLength { get; }
        public float ResourceTriangleHeight { get; }
        public float UniversalResourceStartFactor { get; }
        public float MapSizeFactor { get; } = 1;
        public float MenuRadius { get; } = 140;
        public CCColor4B MenuBackgroundColor { get; } = new CCColor4B(0.5f, 0.5f, 0.5f, 0.5f);
        public float HexagonBorderThickness { get; } = 2;
    }
}