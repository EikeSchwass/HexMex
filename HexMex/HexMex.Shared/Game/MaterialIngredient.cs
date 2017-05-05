using System;

namespace HexMex.Game
{
    public class ResourceIngredient
    {
        public Type ResourceType { get; }
        public int Amount { get; }

        public ResourceIngredient(int amount, Type resourceType)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}