namespace HexMex.Game
{
    public class ResourceTypeSource
    {
        public ResourceType ResourceType { get; }
        public SourceType SourceType { get; }
        public ResourceTypeSource(ResourceType resourceType, SourceType sourceType)
        {
            ResourceType = resourceType;
            SourceType = sourceType;
        }

        public override string ToString() => ResourceType.ToString();
    }
}