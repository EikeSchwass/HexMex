using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HexMex.Game
{
    public class EdgeManager : IEnumerable<Edge>
    {
        public event Action<EdgeManager, Edge> EdgeAdded;

        public WorldSettings WorldSettings { get; }
        private List<Edge> Edges { get; } = new List<Edge>();

        public EdgeManager(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
        }

        public void AddEdge(HexagonNode from, HexagonNode to, float resourceTravelDuration = 1)
        {
            var edge = new Edge(from, to, resourceTravelDuration);
            Edges.Add(edge);
            EdgeAdded?.Invoke(this, edge);
        }

        public bool ContainsEdge(HexagonNode from, HexagonNode to)
        {
            return Edges.Any(e => e.Equals(from, to));
        }

        public IEnumerator<Edge> GetEnumerator()
        {
            return Edges.GetEnumerator();
        }

        public float GetMinTime() => 1;

        public float GetTimeForEdge(HexagonNode from, HexagonNode to)
        {
            foreach (var edge in Edges)
            {
                if (edge.Equals(from, to))
                    return edge.ResourceTravelDuration;
            }
            return 1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}