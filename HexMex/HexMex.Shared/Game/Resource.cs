using CocosSharp;

namespace HexMex.Game
{
    public class Resource
    {
        public CCPoint WorldPosition { get; protected set; }
        public HexagonCornerPosition Position { get; protected set; }
        public ResourceType ResourceType { get; }

        public Resource(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        public void MoveTo(Structure requestingStructure)
        {

        }
    }
}