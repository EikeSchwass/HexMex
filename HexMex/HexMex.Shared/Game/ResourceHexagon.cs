namespace HexMex.Game
{
    public class ResourceHexagon : Hexagon
    {
        public ResourceHexagon(ResourceType resourceType, int amount, HexagonPosition position) : base(position)
        {
            RemainingResources = amount;
            ResourceType = resourceType;
        }

        public ResourceType ResourceType { get; }
        public int RemainingResources { get; protected set; }
    }
}