namespace HexMex.Game.Buildings
{
    public class ResourceSlot
    {
        public bool HasResource { get; set; }
        public ResourceType ResourceType { get; }

        public ResourceSlot(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }
    }
}