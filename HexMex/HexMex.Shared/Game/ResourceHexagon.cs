using System;

namespace HexMex.Game
{
    public class ResourceHexagon : Hexagon
    {
        public ResourceHexagon(Type resourceType, int amount, HexagonPosition position) : base(position)
        {
            RemainingResources = amount;
            ResourceType = resourceType;
        }

        public Type ResourceType { get; }
        public int RemainingResources { get; protected set; }
    }
}