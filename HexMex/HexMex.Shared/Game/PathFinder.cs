using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            throw new NoPathFoundException<HexagonNode>("No path found", start, destination);
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

        private void StructureAdded(StructureManager structureManager, Structure structure)
        {
            var from = structure.Position;
            PathCollection.Add(from, new Dictionary<HexagonNode, Path>());
            foreach (var other in structureManager)
            {
                var to = other.Position;
                var nodes = PathFinding.AStar(from, to).ToArray();
                PathCollection[from].Add(to, new Path(nodes));
            }
        }

        private void StructureRemoved(StructureManager structureManager, Structure structure)
        {
            var innerKeysToRemove = new List<KeyValuePair<HexagonNode, HexagonNode>>();
            var invalidPaths = new List<Path>();
            foreach (var outerKvp in PathCollection)
            {
                foreach (var innerKvp in outerKvp.Value)
                {
                    var path = innerKvp.Value;
                    if (!path.HasNodeInPath(structure.Position))
                        continue;
                    innerKeysToRemove.Add(new KeyValuePair<HexagonNode, HexagonNode>(outerKvp.Key, innerKvp.Key));
                    invalidPaths.Add(path);
                }
            }
            foreach (var keyValuePair in innerKeysToRemove)
            {
                PathCollection[keyValuePair.Key].Remove(keyValuePair.Value);
            }
            foreach (var key in PathCollection.Keys.ToArray())
            {
                if (PathCollection[key].Count == 0)
                    PathCollection.Remove(key);
            }
            foreach (var invalidPath in invalidPaths)
            {
                var newPath = (Path)null;
                try
                {
                    newPath = new Path(PathFinding.AStar(invalidPath.Start, invalidPath.Destination).ToArray());
                    var d = PathCollection.ContainsKey(newPath.Start) ? PathCollection[newPath.Start] : new Dictionary<HexagonNode, Path>();
                    if (d.ContainsKey(newPath.Destination))
                        throw new InvalidOperationException("Invalid paths should have been removed");
                    d.Add(newPath.Destination, newPath);
                }
                catch (NoPathFoundException<HexagonNode>)
                {

                }
                invalidPath.OnPathInvalidate(newPath);
            }
        }
    }

    public class Path
    {
        public event Action<Path, Path> Invalidated;
        public HexagonNode Start { get; }
        public HexagonNode Destination { get; }
        public ReadOnlyCollection<HexagonNode> AllHops { get; }
        public ReadOnlyCollection<HexagonNode> HopsInBetween { get; }

        private Path Reversed { get; set; }

        public Path(params HexagonNode[] nodes)
        {
            AllHops = new ReadOnlyCollection<HexagonNode>(nodes);
            Start = AllHops.First();
            Destination = AllHops.Last();
            if (nodes.Length < 2)
                HopsInBetween = new ReadOnlyCollection<HexagonNode>(new HexagonNode[0]);
            else
            {
                var hib = new HexagonNode[nodes.Length - 2];
                for (int i = 1; i < nodes.Length - 1; i++)
                {
                    hib[i - 1] = nodes[i];
                }
                HopsInBetween = new ReadOnlyCollection<HexagonNode>(hib);
            }
            Reversed = new Path(this, nodes.Reverse().ToArray());
        }

        private Path(Path reversed, params HexagonNode[] nodes)
        {
            AllHops = new ReadOnlyCollection<HexagonNode>(nodes);
            Start = AllHops.First();
            Destination = AllHops.Last();
            if (nodes.Length < 2)
                HopsInBetween = new ReadOnlyCollection<HexagonNode>(new HexagonNode[0]);
            else
            {
                var hib = new HexagonNode[nodes.Length - 2];
                for (int i = 1; i < nodes.Length - 1; i++)
                {
                    hib[i - 1] = nodes[i];
                }
                HopsInBetween = new ReadOnlyCollection<HexagonNode>(hib);
            }
            Reversed = reversed;
        }

        public HexagonNode GetElementAfter(HexagonNode nextNode)
        {
            for (int i = 0; i < AllHops.Count - 1; i++)
            {
                if (Equals(AllHops[i], nextNode))
                    return AllHops[i + 1];
            }
            throw new ArgumentException("No element found/element is last entry", nameof(nextNode));
        }

        public bool HasNodeInPath(HexagonNode node)
        {
            return AllHops.Contains(node);
        }

        public void OnPathInvalidate(Path newPath)
        {
            Invalidated?.Invoke(this, newPath);
        }
        public Path Reverse()
        {
            return Reversed;
        }
    }
}