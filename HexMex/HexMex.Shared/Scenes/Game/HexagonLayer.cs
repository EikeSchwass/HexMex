using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class HexagonLayer : CCLayer
    {
        public HexagonLayer(HexagonManager hexagonManager, WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
            HexagonManager = hexagonManager;
            ZOrder = 0;
            HexagonCorners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, worldSettings.HexagonRadius).ToArray();
            TriangleCornersPointed = GetPointedTriangle(worldSettings.ResourceTriangleHeight, worldSettings.ResourceTriangleEdgeLength);
            TriangleCornersFlat = GetFlatTriangle(worldSettings.ResourceTriangleHeight, worldSettings.ResourceTriangleEdgeLength);

            hexagonManager.HexagonRevealed += HexagonRevealed;
        }

        public HexagonManager HexagonManager { get; }

        public WorldSettings WorldSettings { get; }

        private Dictionary<HexagonPosition, CCDrawNode> DrawNodes { get; } = new Dictionary<HexagonPosition, CCDrawNode>();
        private CCPoint[] HexagonCorners { get; }
        private Dictionary<int, CCPoint[]> TriangleCornerCache { get; } = new Dictionary<int, CCPoint[]>();
        private CCPoint[] TriangleCornersFlat { get; }
        private CCPoint[] TriangleCornersPointed { get; }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            foreach (var drawNode in DrawNodes)
            {
                drawNode.Value.Position = drawNode.Key.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            }
        }

        private void DrawResourceHexagon(ResourceHexagon resourceHexagon)
        {
            var drawNode = GetDrawNode(resourceHexagon.Position);
            var resourceCount = resourceHexagon.RemainingResources;
            for (int i = 0; i < resourceCount; i++)
            {
                var triangleCorners = GetTriangleCorners(i);
                var color = resourceHexagon.ResourceType.GetColor();
                drawNode.DrawPolygon(triangleCorners, 3, color, resourceHexagon.ResourceType.HasBorder() ? 0f : 0, ColorCollection.DefaultResourceBorderColor);
            }
        }

        private CCDrawNode GetDrawNode(HexagonPosition position)
        {
            CCDrawNode drawNode;
            if (!DrawNodes.ContainsKey(position))
            {
                drawNode = new CCDrawNode();
                DrawNodes.Add(position, drawNode);
                AddChild(drawNode);
            }
            else
                drawNode = DrawNodes[position];
            return drawNode;
        }

        private CCPoint[] GetFlatTriangle(float triangleHeight, float edgeLength)
        {
            var result = new[]
            {
                new CCPoint(-edgeLength / 2, 0),
                new CCPoint(edgeLength / 2, 0),
                new CCPoint(0, triangleHeight)
            };
            return result;
        }

        private int GetLayerOfTriangle(int triangleIndex)
        {
            int sqrt = (int)Math.Sqrt(triangleIndex);
            return sqrt;
        }

        private CCPoint[] GetPointedTriangle(float triangleHeight, float edgeLength)
        {
            var result = new[]
            {
                new CCPoint(0, 0),
                new CCPoint(-edgeLength / 2, triangleHeight),
                new CCPoint(edgeLength / 2, triangleHeight)
            };
            return result;
        }

        private CCPoint[] GetTriangleCorners(int index)
        {
            if (TriangleCornerCache.ContainsKey(index))
                return TriangleCornerCache[index];

            int areaIndex = index % 6;
            int indexInArea = index - areaIndex;
            indexInArea /= 6;
            int typeIndex;
            int layer = GetLayerOfTriangle(indexInArea);
            bool isPointed = IsPointed(indexInArea);
            float offsetX;
            var offsetY = layer * WorldSettings.ResourceTriangleHeight;
            CCPoint[] triangleCorners;
            if (isPointed)
            {
                typeIndex = 0;
                for (int i = 0; i < indexInArea; i++)
                {
                    if (IsPointed(i))
                        typeIndex++;
                }
                int indexInLayer = typeIndex - layer * (layer + 1) / 2;
                offsetX = (indexInLayer - layer / 2f) * 2;
                triangleCorners = TriangleCornersPointed;
            }
            else
            {
                typeIndex = 0;
                for (int i = 0; i < indexInArea; i++)
                {
                    if (!IsPointed(i))
                        typeIndex++;
                }
                int indexInLayer = typeIndex - layer * (layer - 1) / 2;
                offsetX = (indexInLayer + 0.5f - layer / 2f) * 2;
                triangleCorners = TriangleCornersFlat;
            }
            offsetX *= WorldSettings.ResourceTriangleEdgeLength / 2;
            var offset = new CCPoint(offsetX, offsetY);
            triangleCorners = triangleCorners.Select(corner => (corner + offset).RotateAround(CCPoint.Zero, areaIndex * 60 + 30)).ToArray();
            TriangleCornerCache.Add(index, triangleCorners);

            return triangleCorners;
        }

        private void HexagonRevealed(HexagonManager sender, Hexagon addedHexagon)
        {
            addedHexagon.RequiresRedraw += RedrawHexagon;
            var ccDrawNode = new CCDrawNode();
            DrawNodes.Add(addedHexagon.Position, ccDrawNode);
            AddChild(ccDrawNode);
            RedrawHexagon(addedHexagon);
        }

        private bool IsPointed(int triangleIndex)
        {
            int sqrt = (int)Math.Sqrt(triangleIndex);
            int pow = sqrt * sqrt;
            return triangleIndex >= pow + sqrt;
        }

        private void RedrawAllHexagons()
        {
            foreach (var hexagon in HexagonManager)
            {
                RedrawHexagon(hexagon);
            }
        }

        private void RedrawHexagon(Hexagon hexagon)
        {
            var drawNode = GetDrawNode(hexagon.Position);
            drawNode.Clear();

            drawNode.DrawPolygon(HexagonCorners, 6, ColorCollection.DefaultHexagonBackgroundColor, 2, ColorCollection.DefaultHexagonBorderColor);
            drawNode.Position = hexagon.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);

            if (hexagon is ResourceHexagon resourceHexagon)
            {
                DrawResourceHexagon(resourceHexagon);
            }
        }
    }
}