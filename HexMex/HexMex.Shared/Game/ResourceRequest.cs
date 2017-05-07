using System;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public partial class ResourceRequest
    {
        private ResourceRequest(ResourceType type, Structure structure, RequestPriority priority)
        {
            Type = type;
            Priority = priority;
            RequestingStructure = structure;
        }

        public ResourcePackage Resource { get; private set; }
        public RequestPriority Priority { get; set; }
        public Structure RequestingStructure { get; }

        public ResourceRequestState RequestState { get; private set; }
        public ResourceType Type { get; }

        private static Func<ResourceRequest, ResourceRequestChanger> ResourceRequestChangerFactory { get; set; }

        public static ResourceRequestChanger CreateResourceRequest(ResourceType resourceType, Structure structure, RequestPriority priority)
        {
            if (ResourceRequestChangerFactory == null)
                ResourceRequestChanger.SetFactory();
            var resourceRequest = new ResourceRequest(resourceType, structure, priority);
            // ReSharper disable once PossibleNullReferenceException
            var requestChanger = ResourceRequestChangerFactory(resourceRequest);
            return requestChanger;
        }
    }
}