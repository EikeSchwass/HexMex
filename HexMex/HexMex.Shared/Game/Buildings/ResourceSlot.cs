namespace HexMex.Game.Buildings
{
    public class ResourceSlot
    {
        public ResourceType ResourceType { get; }
        public bool HasResource { get; set; }

        public ResourceSlot(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }
    }
}