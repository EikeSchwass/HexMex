using System;
using System.Collections.Generic;
using HexMex.Helper;

namespace HexMex.Game
{
    public class PathFinder
    {
        public event Action<HexagonNode> NodeRemoved;
        private EdgeManager EdgeManager { get; }
        private HexagonManager HexagonManager { get; }
        private StructureManager StructureManager { get; }

        private Dictionary<HexagonNode, Dictionary<HexagonNode, HexagonNode[]>> CachedPath { get; } = new Dictionary<HexagonNode, Dictionary<HexagonNode, HexagonNode[]>>();

        private PathFinding<HexagonNode, float> PathFinding { get; }

        public PathFinder(HexagonManager hexagonManager, EdgeManager edgeManager, StructureManager structureManager)
        {
            HexagonManager = hexagonManager;
            EdgeManager = edgeManager;
            StructureManager = structureManager;
            PathFinding = new PathFinding<HexagonNode, float>((i, j) => i + j, CalculateAdjacentNodes, CalculateHeuristik, CalculateCostOfEdge);
        }

        public HexagonNode[] FindPath(HexagonNode from, HexagonNode to)
        {
            if (from == to)
                return new[]
                {
                    from
                };

            if (CachedPath.ContainsKey(from))
            {
                if (!CachedPath[from].ContainsKey(to))
                    CachedPath[from].Add(to, PathFinding.AStar(from, to).ToArray());
            }
            else
            {
                CachedPath.Add(from, new Dictionary<HexagonNode, HexagonNode[]>
                {
                    {to, PathFinding.AStar(from, to).ToArray()}
                });
            }
            return CachedPath[from][to];
        }

        public void OnNodeRemoved(HexagonNode removedNode)
        {
            NodeRemoved?.Invoke(removedNode);
        }

        private IEnumerable<HexagonNode> CalculateAdjacentNodes(HexagonNode node)
        {
            var adjacentHexagonNodes = node.GetAccessibleAdjacentHexagonNodes(HexagonManager);
            foreach (var adjacentHexagonNode in adjacentHexagonNodes)
            {
                if (EdgeManager.ContainsEdge(node, adjacentHexagonNode))
                    yield return adjacentHexagonNode;
            }
        }

        private float CalculateCostOfEdge(HexagonNode node1, HexagonNode node2)
        {
            return EdgeManager.GetTimeForEdge(node1, node2);
        }

        private float CalculateHeuristik(HexagonNode node, HexagonNode destination)
        {
            var h1 = new[]
            {
                node.Position1,
                node.Position2,
                node.Position3
            };
            var h2 = new[]
            {
                destination.Position1,
                destination.Position2,
                destination.Position3
            };

            int min = int.MaxValue;
            foreach (var from in h1)
            {
                foreach (var to in h2)
                {
                    var distance = (to - from).DistanceToOrigin;
                    if (min > distance)
                        min = distance;
                }
            }
            return min * EdgeManager.GetMinTime();
        }
    }
}