using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;

namespace HexMex.Helper
{
    public static class HexagonHelper
    {
        public const float Rad60 = (float)(Math.PI * 2 / 6);
        public const float Rad30 = (float)(Math.PI / 6);
        public static readonly float RadiusToHeightFactor = (float)(Math.Sqrt(3) / 2);
        public static readonly float Sin30 = (float)Math.Sin(Rad30);
        public static readonly float Cos30 = (float)Math.Cos(Rad30);
        public static readonly float Sin60 = (float)Math.Sin(Rad60);
        public static readonly float Cos60 = (float)Math.Cos(Rad60);

        public static HexagonPosition[] AdjacentHexagonPositionOffsets { get; } = {new HexagonPosition(1, -1, 0), new HexagonPosition(1, 0, -1), new HexagonPosition(-1, 1, 0), new HexagonPosition(-1, 0, 1), new HexagonPosition(0, -1, 1), new HexagonPosition(0, 1, -1)};

        public static CCPoint CalculateWorldPosition(int x, int y, int z, float radius, float margin)
        {
            radius += margin;
            float height = radius * RadiusToHeightFactor;
            float xPos = x * height * 2;
            float yPos = Cos30 * height * 2 * z;
            xPos += Sin30 * height * 2 * z;
            return new CCPoint(xPos, yPos);
        }

        public static IEnumerable<CCPoint> GenerateWorldCorners(CCPoint center, float radius)
        {
            for (int i = 0; i < 6; i++)
            {
                double angle = i * Rad60;
                float x = (float)(Math.Sin(angle) * radius + center.X);
                float y = (float)(Math.Cos(angle) * radius + center.Y);
                var corner = new CCPoint(x, y);
                yield return corner;
            }
        }

        public static bool IsPointInsidePolygon(IList<CCPoint> corners, CCPoint pointToTest)
        {
            var testX = pointToTest.X;
            var testY = pointToTest.Y;
            var vertexX = corners.Select(corner => corner.X).ToArray();
            var vertexY = corners.Select(corner => corner.Y).ToArray();
            var vertexCount = vertexX.Length;
            bool c = false;
            for (int i = 0, j = vertexCount - 1; i < vertexCount; j = i++)
            {
                if (vertexY[i] > testY != vertexY[j] > testY && testX < (vertexX[j] - vertexX[i]) * (testY - vertexY[i]) / (vertexY[j] - vertexY[i]) + vertexX[i])
                    c = !c;
            }
            return c;
        }
    }
}