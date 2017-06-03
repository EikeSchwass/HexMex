using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game
{
    public struct HexagonNode
    {
        public HexagonPosition Position1 { get; }
        public HexagonPosition Position2 { get; }
        public HexagonPosition Position3 { get; }
        public bool IsYShape { get; }

        // TODO Optimize
        public HexagonNode(HexagonPosition position1, HexagonPosition position2, HexagonPosition position3)
        {
            if (position1 == position2 || position1 == position3 || position2 == position3)
                throw new ArgumentException("The hexagon positions have to be distinct from one another ");

            var positions = new[] {position1, position2, position3};
            positions = positions.OrderBy(p => p.X).ThenBy(p => p.Y).ThenBy(p => p.Z).ToArray();
            Position1 = positions[0];
            Position2 = positions[1];
            Position3 = positions[2];

            IsYShape = false;
            int singleZ = positions.Unique(p => p.Z).First();
            foreach (var hexagonPosition in positions)
            {
                if (hexagonPosition.Z != singleZ)
                {
                    IsYShape = hexagonPosition.Z <= singleZ;
                    break;
                }
            }
        }

        public bool Equals(HexagonNode other)
        {
            return Position1.Equals(other.Position1) && Position2.Equals(other.Position2) && Position3.Equals(other.Position3);
        }

        // TODO Optimize
        public IEnumerable<HexagonNode> GetAdjacentHexagonNodes()
        {
            var positions = new[] {Position1, Position2, Position3};

            var adj1 = Position1.GetAdjacentHexagonPositions();
            var adj2 = Position2.GetAdjacentHexagonPositions();
            var adj3 = Position3.GetAdjacentHexagonPositions();

            var new3 = adj1.Intersect(adj2).First(a => !positions.Contains(a));
            var new2 = adj1.Intersect(adj3).First(a => !positions.Contains(a));
            var new1 = adj2.Intersect(adj3).First(a => !positions.Contains(a));
            var result = new[] {new HexagonNode(new1, Position2, Position3), new HexagonNode(Position1, new2, Position3), new HexagonNode(Position1, Position2, new3)};
            return result;
        }

        public IEnumerable<HexagonNode> GetAccessibleAdjacentHexagonNodes(HexagonManager hexagonManager)
        {
            var hexPos = new[] {Position1, Position2, Position3};
            var adjacent = GetAdjacentHexagonNodes();
            foreach (var hexagonNode in adjacent)
            {
                var otherPos = new[] {hexagonNode.Position1, hexagonNode.Position2, hexagonNode.Position3};
                var hexagonPositions = hexPos.Intersect(otherPos).ToArray();
                if (hexagonManager.CanReachThrough(hexagonPositions[0], hexagonPositions[1]))
                    yield return hexagonNode;
            }
        }

        public override string ToString()
        {
            return $"{Position1},{Position2},{Position3}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is HexagonNode && Equals((HexagonNode)obj);
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

        public static bool operator ==(HexagonNode left, HexagonNode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexagonNode left, HexagonNode right)
        {
            return !left.Equals(right);
        }

        public CCPoint GetWorldPosition(float hexRadius, float hexMargin)
        {
            var p1 = Position1.GetWorldPosition(hexRadius, hexMargin);
            var p2 = Position2.GetWorldPosition(hexRadius, hexMargin);
            var p3 = Position3.GetWorldPosition(hexRadius, hexMargin);
            var result = (p1 + p2 + p3) / 3;
            return result;
        }
    }
}