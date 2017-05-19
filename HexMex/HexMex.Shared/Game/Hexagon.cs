using System;

namespace HexMex.Game
{
    public class Hexagon : IRenderable<Hexagon>
    {
        public event Action<Hexagon> RequiresRedraw;

        public HexagonPosition Position { get; }

        public ResourceType ResourceType { get; }

        public int PayoutSum { get; }

        public Hexagon(ResourceType resourceType, int payoutSum, HexagonPosition position)
        {
            PayoutSum = payoutSum;
            ResourceType = resourceType;
            Position = position;
        }
    }
}