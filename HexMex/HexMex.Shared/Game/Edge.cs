namespace HexMex.Game
{
    public class Edge
    {
        public Edge(HexagonNode from, HexagonNode to, float resourceTravelDuration)
        {
            From = from;
            To = to;
            ResourceTravelDuration = resourceTravelDuration;
        }

        public HexagonNode From { get; }
        public float ResourceTravelDuration { get; }
        public HexagonNode To { get; }

        public bool Equals(HexagonNode node1, HexagonNode node2) => node1 == From && node2 == To || node1 == To && node2 == From;
    }
}