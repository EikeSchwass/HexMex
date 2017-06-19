using System.Collections.Generic;
using System.Linq;
using HexMex.Game.Settings;
using HexMex.Helper;

namespace HexMex.Game
{
    /*public class PathFinder
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
    */
    public class CachedPathFinder
    {
        public GameplaySettings GameplaySettings { get; }
        private Dictionary<HexagonNode, Dictionary<HexagonNode, Path>> PathCollection { get; } = new Dictionary<HexagonNode, Dictionary<HexagonNode, Path>>();
        private List<Path> Paths { get; } = new List<Path>();

        private HexagonManager HexagonManager { get; }
        private StructureManager StructureManager { get; }
        private PathFinding<HexagonNode, float> PathFinding { get; }

        public CachedPathFinder(HexagonManager hexagonManager, StructureManager structureManager, GameplaySettings gameplaySettings)
        {
            PathFinding = new PathFinding<HexagonNode, float>((i, j) => i + j, h => h.GetAccessibleAdjacentHexagonNodes(HexagonManager).Where(a => StructureManager[a] != null), CalculateHeuristik, (c, n) => gameplaySettings.DefaultResourceTimeBetweenNodes);
            HexagonManager = hexagonManager;
            StructureManager = structureManager;
            GameplaySettings = gameplaySettings;
            StructureManager.StructureAdded += StructureAdded;
            StructureManager.StructureRemoved += StructureRemoved;
        }

        public Path GetPath(HexagonNode start, HexagonNode destination)
        {
            if (PathCollection.ContainsKey(start) && PathCollection[start].ContainsKey(destination))
                return PathCollection[start][destination];
            if (PathCollection.ContainsKey(destination) && PathCollection[destination].ContainsKey(start))
                return PathCollection[destination][start].Reverse();
            return null;
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
            return min * GameplaySettings.DefaultResourceTimeBetweenNodes;
        }

        private void RecalculateAllPaths()
        {
            PathCollection.Clear();
            foreach (var start in StructureManager)
            {
                if (!PathCollection.ContainsKey(start.Position))
                    PathCollection.Add(start.Position, new Dictionary<HexagonNode, Path>());
                foreach (var destination in StructureManager)
                {
                    try
                    {
                        if (!PathCollection[start.Position].ContainsKey(destination.Position))
                        {
                            Path path = new Path(PathFinding.AStar(start.Position, destination.Position).ToArray());
                            AddPath(path);
                            if (path.AllHops.Count > 2)
                            {
                                var containedPaths = path.GetContainedPaths();
                                foreach (var containedPath in containedPaths)
                                {
                                    AddPath(containedPath);
                                }
                            }
                        }
                    }
                    catch (NoPathFoundException<HexagonNode>) { }
                }
            }
        }
        private void AddPath(Path path)
        {
            if (!PathCollection.ContainsKey(path.Start))
                PathCollection.Add(path.Start, new Dictionary<HexagonNode, Path>());
            if (!PathCollection[path.Start].ContainsKey(path.Destination))
                PathCollection[path.Start].Add(path.Destination, path);
            
        }

        private void StructureAdded(StructureManager structureManager, Structure structure)
        {
            RecalculateAllPaths();
        }

        private void StructureRemoved(StructureManager structureManager, Structure structure)
        {
            RecalculateAllPaths();
        }
    }
}