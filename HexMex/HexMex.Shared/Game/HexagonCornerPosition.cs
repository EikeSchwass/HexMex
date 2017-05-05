using System;
using System.Linq;

namespace HexMex.Game
{
    public struct HexagonCornerPosition
    {
        public HexagonPosition Position1 { get; }
        public HexagonPosition Position2 { get; }
        public HexagonPosition Position3 { get; }

        public HexagonCornerPosition(HexagonPosition position1, HexagonPosition position2, HexagonPosition position3)
        {
            if (position1 == position2 || position1 == position3 || position2 == position3)
                throw new ArgumentException("The hexagon positions have to be distinct from one another ");

            var positions = new[]
            {
                position1,
                position2,
                position3
            };
            positions = positions.OrderBy(p => p.X).ThenBy(p => p.Y).ThenBy(p => p.Z).ToArray();
            Position1 = positions[0];
            Position2 = positions[1];
            Position3 = positions[2];
        }

        public bool Equals(HexagonCornerPosition other)
        {
            return Position1.Equals(other.Position1) && Position2.Equals(other.Position2) && Position3.Equals(other.Position3);
        }

        public HexagonCornerPosition[] GetAdjacentCornerPositions()
        {
            var adj1 = Position1.GetAdjacentHexagonPositions();
            var adj2 = Position2.GetAdjacentHexagonPositions();
            var adj3 = Position3.GetAdjacentHexagonPositions();

            var new3 = adj1.Intersect(adj2).First();
            var new2 = adj1.Intersect(adj3).First();
            var new1 = adj2.Intersect(adj3).First();
            var result = new[]
            {
                new HexagonCornerPosition(new1, Position2, Position3),
                new HexagonCornerPosition(Position1, new2, Position3),
                new HexagonCornerPosition(Position1, Position2, new3),
            };
            return result;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is HexagonCornerPosition && Equals((HexagonCornerPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position1.GetHashCode();
                hashCode = (hashCode * 397) ^ Position2.GetHashCode();
                hashCode = (hashCode * 397) ^ Position3.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(HexagonCornerPosition left, HexagonCornerPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexagonCornerPosition left, HexagonCornerPosition right)
        {
            return !left.Equals(right);
        }
    }
}