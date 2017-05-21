using System;
using CocosSharp;

namespace HexMex.Game
{
    public class Hexagon : ICCUpdatable
    {
        public HexagonPosition Position { get; }

        public ResourceType ResourceType { get; }

        public float PayoutInterval { get; }
        public float TimeSinceLastPayout { get; private set; }
        public event Action<Hexagon, ResourceType> Payout;

        public Hexagon(ResourceType resourceType, float payoutInterval, HexagonPosition position)
        {
            PayoutInterval = payoutInterval;
            ResourceType = resourceType;
            Position = position;
        }

        public void Update(float dt)
        {
            TimeSinceLastPayout += dt;
            if (TimeSinceLastPayout >= PayoutInterval)
            {
                TimeSinceLastPayout = 0;
                Payout?.Invoke(this, ResourceType);
            }
        }


    }
}