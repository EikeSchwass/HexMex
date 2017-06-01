using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HexMex.Game
{
    public class ResourceDirector
    {
        public event Action<ResourceDirector, ResourceType[]> AllIngredientsArrived;
        public event Action<ResourceDirector> AllProvisionsLeft;

        public ReadOnlyCollection<ResourceType> ArrivedResources => ArrivedResourceList.AsReadOnly();
        public ReadOnlyCollection<ResourceType> PendingProvisions => ProvidedResourceList.Select(r => r.ResourceType).ToList().AsReadOnly();
        public ReadOnlyCollection<ResourceType> PendingRequests => RequestedNetworkResourceList.Select(r => r.ResourceType).Concat(RequestedHexagonResourceList).ToList().AsReadOnly();
        public RequestPriority Priority { get; private set; } = RequestPriority.Normal;
        public Structure Structure { get; }
        public World World { get; }

        public bool ReadyForProduction => !ProvidedResourceList.Any() && !RequestedNetworkResourceList.Any() && !RequestedHexagonResourceList.Any();

        private List<ResourceType> ArrivedResourceList { get; } = new List<ResourceType>();
        private List<ResourcePackage> ProvidedResourceList { get; } = new List<ResourcePackage>();
        private List<ResourcePackage> RequestedNetworkResourceList { get; } = new List<ResourcePackage>();
        private List<ResourceType> RequestedHexagonResourceList { get; } = new List<ResourceType>();
        private bool HasAdjacentWater { get; }

        public ResourceDirector(Structure structure)
        {
            Structure = structure;
            World = structure.World;
            var h1 = World.HexagonManager[Structure.Position.Position1];
            var h2 = World.HexagonManager[Structure.Position.Position2];
            var h3 = World.HexagonManager[Structure.Position.Position3];
            HasAdjacentWater = h1.ResourceType == ResourceType.PureWater || h2.ResourceType == ResourceType.PureWater || h3.ResourceType == ResourceType.PureWater;
        }

        public void AdjacentHexagonProvidedResource(ResourceType resourceType)
        {
            if (RequestedHexagonResourceList.Contains(resourceType))
            {
                RequestedHexagonResourceList.Remove(resourceType);
                NewResourceAvailable();
            }
        }

        public void ProvideResources(params ResourceType[] resourceTypes)
        {
            if (ProvidedResourceList.Any())
                throw new InvalidOperationException("Can't provide new Resources until all currently provided resources are on it's way");
            foreach (var resourceType in resourceTypes)
            {
                var resourcePackage = World.ResourceManager.ProvideResource(Structure, resourceType, Priority);
                ProvidedResourceList.Add(resourcePackage);
                if (resourcePackage.ResourceRequestState != ResourceRequestState.Pending)
                    ResourcePackageStartedMoving(resourcePackage);
                else
                    resourcePackage.StartedMoving += ResourcePackageStartedMoving;
            }
        }

        public void RequestIngredients(ResourceType[] networkResourceTypes, ResourceType[] extractionResourceTypes)
        {
            if (RequestedNetworkResourceList.Any() || RequestedHexagonResourceList.Any())
                throw new InvalidOperationException("Can't request new Resources until all current requests are completed");
            if (networkResourceTypes?.Contains(ResourceType.Water) == true)
            {
                if (HasAdjacentWater && Structure.CanExtractWaterFromAdjacentHexagons)
                {
                    var waterCount = networkResourceTypes.Count(r => r == ResourceType.PureWater || r == ResourceType.Water);
                    networkResourceTypes = networkResourceTypes.Where(r => r != ResourceType.Water && r != ResourceType.PureWater).ToArray();
                    extractionResourceTypes = (extractionResourceTypes ?? new ResourceType[0]).Concat(Enumerable.Repeat(ResourceType.PureWater, waterCount)).ToArray();
                }
            }
            foreach (var resourceType in networkResourceTypes ?? Enumerable.Empty<ResourceType>())
            {
                var resourcePackage = World.ResourceManager.RequestResource(Structure, resourceType, Priority);
                RequestedNetworkResourceList.Add(resourcePackage);
            }
            RequestedHexagonResourceList.AddRange(extractionResourceTypes ?? Enumerable.Empty<ResourceType>());
        }

        public void ResourceArrived(ResourcePackage resourcePackage)
        {
            RequestedNetworkResourceList.Remove(resourcePackage);
            ArrivedResourceList.Add(resourcePackage.ResourceType);
            NewResourceAvailable();
        }

        public void ResourcePassesThrough(ResourcePackage resourcePackage) { }

        public void SetPriority(RequestPriority newPriority)
        {
            Priority = newPriority;
            foreach (var resourcePackage in RequestedNetworkResourceList)
            {
                World.ResourceManager.UpdateRequestPriority(resourcePackage, Priority);
            }
            foreach (var resourcePackage in ProvidedResourceList)
            {
                World.ResourceManager.UpdateProvisionPriority(resourcePackage, Priority);
            }
        }

        private void NewResourceAvailable()
        {
            if (RequestedNetworkResourceList.Count == 0 && RequestedHexagonResourceList.Count == 0)
            {
                AllIngredientsArrived?.Invoke(this, ArrivedResourceList.ToArray());
                ArrivedResourceList.Clear();
            }
        }

        private void ResourcePackageStartedMoving(ResourcePackage providedResourcePackage)
        {
            ProvidedResourceList.Remove(providedResourcePackage);
            if (ProvidedResourceList.Count == 0)
            {
                AllProvisionsLeft?.Invoke(this);
            }
        }
    }
}