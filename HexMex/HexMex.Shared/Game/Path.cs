using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HexMex.Game
{
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