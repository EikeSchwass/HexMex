using System;

namespace HexMex.Game
{
    public class ResourceHexagon : Hexagon
    {
        private int remainingResources;

        public ResourceHexagon(ResourceType resourceType, int amount, HexagonPosition position) : base(position)
        {
            RemainingResources = amount;
            ResourceType = resourceType;
        }

        public override event Action<Hexagon> RequiresRedraw;

        public int RemainingResources
        {
            get => remainingResources;
            set
            {
                remainingResources = value;
                RequiresRedraw?.Invoke(this);
            }
        }

        public ResourceType ResourceType { get; }
    }
}