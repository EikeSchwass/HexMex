using System;
using System.Collections.Generic;
using HexMex.Helper;

namespace HexMex.Game
{
    public class PathFinder
    {
        private HexagonManager HexagonManager { get; }
        private EdgeManager EdgeManager { get; }
        private StructureManager StructureManager { get; }

        public PathFinder(HexagonManager hexagonManager, EdgeManager edgeManager, StructureManager structureManager)
        {
            HexagonManager = hexagonManager;
            EdgeManager = edgeManager;
            StructureManager = structureManager;
        }

        public HexagonNode[] FindPath(HexagonNode from, HexagonNode to)
        {
            return PathFinding.AStar(from, to, (i, j) => i + j, n => CalculateAdjacentNodes(n, to), CalculateHeuristik, CalculateCostOfEdge).ToArray();
        }

        private IEnumerable<HexagonNode> CalculateAdjacentNodes(HexagonNode node, HexagonNode destination)
        {
            var adjacentHexagonNodes = node.GetAccessibleAdjacentHexagonNodes(HexagonManager);
            foreach (var adjacentHexagonNode in adjacentHexagonNodes)
            {
                if (StructureManager.GetStructureAtPosition(adjacentHexagonNode) != null || adjacentHexagonNode == destination)
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
                    var distance = (to - @from).DistanceToOrigin;
                    if (min > distance)
                        min = distance;
                }
            }
            return min * EdgeManager.GetMinTime();
        }

        public void OnNodeRemoved(HexagonNode removedNode)
        {
            NodeRemoved?.Invoke(removedNode);
        }

        public event Action<HexagonNode> NodeRemoved;
    }
}