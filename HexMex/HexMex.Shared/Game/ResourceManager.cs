using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    // TODO Cleanup
    public class ResourceManager : ICCUpdatable
    {
        public ResourceManager(World world)
        {
            RequestCollection = new RequestCollection();
            CheckInterval = world.WorldSettings.ResourceManagerUpdateInterval;
        }

        public float CheckInterval { get; set; }
        private List<ResourceProvision.ResourceProvisionChanger> ProvidedResources { get; } = new List<ResourceProvision.ResourceProvisionChanger>();
        private RequestCollection RequestCollection { get; }
        private float TimeSinceLastCheck { get; set; }

        public ResourceProvision ProvideResource(ResourceType resourceType, Structure structure, RequestPriority priority)
        {
            ResourceProvision.ResourceProvisionChanger resourceProvisionChanger = ResourceProvision.CreateResourceProvision(resourceType, structure, priority);
            ProvidedResources.Add(resourceProvisionChanger);
            return resourceProvisionChanger.ResourceProvision;
        }

        public ResourceRequest RequestResource(ResourceType resourceType, Structure structure, RequestPriority priority)
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
                var type = providedResource.ResourceProvision.ResourceType;
                var nextRequest = RequestCollection.GetNextRequestThatWishes(type);
                if (nextRequest == null)
                    continue;
                nextRequest.SetRequestState(ResourceRequestState.OnItsWay);
                providedResource.SetRequestState(ResourceRequestState.OnItsWay);
                //providedResource.ResourceProvision.Resource.MoveTo(nextRequest.ResourceRequest.RequestingStructure);
            }
        }
    }
}