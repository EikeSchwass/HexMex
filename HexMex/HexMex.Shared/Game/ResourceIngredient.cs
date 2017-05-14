namespace HexMex.Game
{
    public class ResourceIngredient
    {
        public int Amount { get; }
        public ResourceType ResourceType { get; }

        public ResourceIngredient(int amount, ResourceType resourceType)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}