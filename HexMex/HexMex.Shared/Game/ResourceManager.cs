using System.Collections.Generic;
using System.Linq;
using CocosSharp;

namespace HexMex.Game
{
    public class ResourceManager : ICCUpdatable
    {
        public ResourceManager(float checkInterval)
        {
            RequestCollection = new RequestCollection();
            CheckInterval = checkInterval;
        }

        public float CheckInterval { get; set; }
        private List<ResourceProvision.ResourceProvisionChanger> ProvidedResources { get; } = new List<ResourceProvision.ResourceProvisionChanger>();
        private RequestCollection RequestCollection { get; }
        private float TimeSinceLastCheck { get; set; }

        public ResourceProvision ProvideResource<T>(T resource) where T : Resource
        {
            ResourceProvision.ResourceProvisionChanger resourceProvisionChanger = ResourceProvision.CreateResourceProvision(resource);
            ProvidedResources.Add(resourceProvisionChanger);
            return resourceProvisionChanger.ResourceProvision;
        }

        public ResourceProvision ProvideResource(Resource resource)
        {
            ResourceProvision.ResourceProvisionChanger resourceProvisionChanger = ResourceProvision.CreateResourceProvision(resource);
            ProvidedResources.Add(resourceProvisionChanger);
            return resourceProvisionChanger.ResourceProvision;
        }

        public ResourceRequest RequestResource(ResourceType resourceType, Structure structure, int priority)
        {
            var request = ResourceRequest.CreateResourceRequest(resourceType, structure, priority);
            RequestCollection.Add(request);
            return request.ResourceRequest;
        }

        public void Update(float dt)
        {
            TimeSinceLastCheck += dt;
            if (TimeSinceLastCheck >= CheckInterval)
            {
                UpdateRequests();
                TimeSinceLastCheck = 0;
            }
        }

        private void UpdateRequests()
        {
            foreach (var providedResource in ProvidedResources.Where(m => m.ResourceProvision.RequestState == ResourceRequestState.Pending))
            {
                var type = providedResource.ResourceProvision.Resource.ResourceType;
                var nextRequest = RequestCollection.GetNextRequestThatWishes(type);
                if (nextRequest == null)
                    continue;
                nextRequest.SetRequestState(ResourceRequestState.OnItsWay);
                providedResource.SetRequestState(ResourceRequestState.OnItsWay);
                providedResource.ResourceProvision.Resource.MoveTo(nextRequest.ResourceRequest.RequestingStructure);
            }
        }
    }
}