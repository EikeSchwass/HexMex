namespace HexMex.Game
{
    public class ResourceIngredient
    {
        public ResourceType ResourceType { get; }
        public int Amount { get; }

        public ResourceIngredient(int amount, ResourceType resourceType)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}