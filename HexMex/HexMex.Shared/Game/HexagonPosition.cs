using System;
using System.Linq;
using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game
{
    public struct HexagonPosition
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        private CCPoint WorldPositionRadiusOne { get; }
        public static HexagonPosition Zero { get; } = new HexagonPosition(0, 0, 0);

        public HexagonPosition(int x, int y, int z)
        {
            if (x + y + z != 0)
                throw new ArgumentException("The sum of all axis has to be 0");
            X = x;
            Y = y;
            Z = z;
            WorldPositionRadiusOne = HexagonHelper.CalculateWorldPosition(x, y, z, 1);
        }

        public bool Equals(HexagonPosition other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is HexagonPosition && Equals((HexagonPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }

        public HexagonPosition[] GetAdjacentHexagonPositions()
        {
            var local = this;
            return HexagonHelper.AdjacentHexagonPositionOffsets.Select(p => p + local).ToArray();
        }

        public static bool operator ==(HexagonPosition left, HexagonPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexagonPosition left, HexagonPosition right)
        {
            return !left.Equals(right);
        }

        public static HexagonPosition operator +(HexagonPosition position, HexagonPosition offset)
        {
            int x = position.X + offset.X;
            int y = position.Y + offset.Y;
            int z = position.Z + offset.Z;
            return new HexagonPosition(x, y, z);
        }

        public static HexagonPosition operator -(HexagonPosition position, HexagonPosition offset)
        {
            int x = position.X - offset.X;
            int y = position.Y - offset.Y;
            int z = position.Z - offset.Z;
            return new HexagonPosition(x, y, z);
        }

        public static HexagonPosition operator *(HexagonPosition position, int factor)
        {
            int x = position.X * factor;
            int y = position.Y * factor;
            int z = position.Z * factor;
            return new HexagonPosition(x, y, z);
        }

        public CCPoint GetWorldPosition(float hexRadius)
        {
            return WorldPositionRadiusOne * hexRadius;
        }
    }
}