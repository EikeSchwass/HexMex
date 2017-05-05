using System;

namespace HexMex.Game
{
    public partial class ResourceProvision
    {
        private ResourceProvision(Resource resource)
        {
            Resource = resource;
            RequestState = ResourceRequestState.Pending;
        }

        public Resource Resource { get; }

        public ResourceRequestState RequestState { get; private set; }

        private static Func<ResourceProvision, ResourceProvisionChanger> ResourceProvisionChangerFactory { get; set; }

        public static ResourceProvisionChanger CreateResourceProvision(Resource resource)
        {
            var resourceProvision = new ResourceProvision(resource);
            var provisionChanger = ResourceProvisionChangerFactory(resourceProvision);
            return provisionChanger;
        }
    }
}