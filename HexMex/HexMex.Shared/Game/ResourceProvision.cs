using System;

namespace HexMex.Game
{
    public partial class ResourceProvision
    {
        private ResourceRequestState requestState;

        public event Action<ResourceProvision> ProvisionAccepted;

        public RequestPriority Priority { get; }

        public ResourceRequestState RequestState
        {
            get => requestState;
            private set
            {
                if (requestState == ResourceRequestState.Pending && value == ResourceRequestState.OnItsWay)
                {
                    requestState = value;
                    ProvisionAccepted?.Invoke(this);
                }
                requestState = value;
            }
        }

        public ResourceType ResourceType { get; }
        public Structure Structure { get; }

        private static Func<ResourceProvision, ResourceProvisionChanger> ResourceProvisionChangerFactory { get; set; }

        private ResourceProvision(ResourceType resourceType, Structure structure, RequestPriority priority)
        {
            ResourceType = resourceType;
            RequestState = ResourceRequestState.Pending;
            Structure = structure;
            Priority = priority;
        }

        public static ResourceProvisionChanger CreateResourceProvision(ResourceType resourceType, Structure structure, RequestPriority priority)
        {
            if (ResourceProvisionChangerFactory == null)
                ResourceProvisionChanger.SetFactory();
            var resourceProvision = new ResourceProvision(resourceType, structure, priority);
            // ReSharper disable once PossibleNullReferenceException
            var provisionChanger = ResourceProvisionChangerFactory(resourceProvision);
            return provisionChanger;
        }
    }
}