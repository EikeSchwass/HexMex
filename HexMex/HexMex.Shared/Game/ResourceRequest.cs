using System;

namespace HexMex.Game
{
    public partial class ResourceRequest
    {
        public RequestPriority Priority { get; set; }
        public Structure RequestingStructure { get; }

        public ResourceRequestState RequestState { get; private set; }

        public ResourcePackage Resource { get; private set; }
        public ResourceType Type { get; }

        private static Func<ResourceRequest, ResourceRequestChanger> ResourceRequestChangerFactory { get; set; }

        private ResourceRequest(ResourceType type, Structure structure, RequestPriority priority)
        {
            Type = type;
            Priority = priority;
            RequestingStructure = structure;
        }

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