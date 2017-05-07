using System;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public partial class ResourceProvision
    {
        private ResourceProvision(ResourceType resourceType, Structure structure, RequestPriority priority)
        {
            ResourceType = resourceType;
            RequestState = ResourceRequestState.Pending;
            Structure = structure;
            Priority = priority;
        }

        public RequestPriority Priority { get; }
        public ResourceRequestState RequestState { get; private set; }

        public ResourceType ResourceType { get; }
        public Structure Structure { get; }

        private static Func<ResourceProvision, ResourceProvisionChanger> ResourceProvisionChangerFactory { get; set; }

        public static ResourceProvisionChanger CreateResourceProvision(ResourceType resourceType, Structure structure, RequestPriority priority)
        {
            var resourceProvision = new ResourceProvision(resourceType, structure, priority);
            var provisionChanger = ResourceProvisionChangerFactory(resourceProvision);
            return provisionChanger;
        }
    }
}