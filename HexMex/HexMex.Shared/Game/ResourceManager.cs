using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game
{
    // TODO Cleanup
    public class ResourceManager : ICCUpdatable
    {
        public event Action<ResourceManager, ResourcePackage> PackageArrived;
        public event Action<ResourceManager, ResourcePackage> PackageStarted;

        public ReadOnlyCollection<ResourcePackage> AllPackages => AllPackageList.AsReadOnly();
        public World World { get; }

        private List<ResourcePackage> AllPackageList { get; } = new List<ResourcePackage>();
        private ResourcePriorityQueue<ResourcePackage, ResourceType> Provisions { get; } = new ResourcePriorityQueue<ResourcePackage, ResourceType>((p, r) => p.ResourceType.CanBeUsedFor(r));
        private ResourcePriorityQueue<ResourcePackage, ResourceType> Requests { get; } = new ResourcePriorityQueue<ResourcePackage, ResourceType>((p, r) => r.CanBeUsedFor(p.ResourceType));

        public ResourceManager(World world)
        {
            World = world;
        }

        public ResourcePackage ProvideResource(Structure providingStructure, ResourceType resourceType, RequestPriority priority)
        {
            var requestPackage = Requests.Dequeue(resourceType, rp => HasPathFilter(providingStructure, rp.DestinationStructure));
            if (requestPackage != null)
            {
                requestPackage.StartStructure = providingStructure;
                requestPackage.SpecifyResourceType(resourceType);
                requestPackage.Move();
                return requestPackage;
            }
            var resourcePackage = new ResourcePackage(resourceType, World.PathFinder, World.GameSettings.GameplaySettings) {StartStructure = providingStructure};
            AddPackage(resourcePackage);
            Provisions.Enqueue(resourcePackage, priority);
            return resourcePackage;
        }

        public ResourcePackage RequestResource(Structure requestingStructure, ResourceType resourceType, RequestPriority priority)
        {
            var providedPackage = Provisions.Dequeue(resourceType, rp => HasPathFilter(rp.StartStructure, requestingStructure));
            if (providedPackage != null)
            {
                providedPackage.DestinationStructure = requestingStructure;
                providedPackage.SpecifyResourceType(resourceType);
                providedPackage.Move();
                return providedPackage;
            }
            var resourcePackage = new ResourcePackage(resourceType, World.PathFinder, World.GameSettings.GameplaySettings) {DestinationStructure = requestingStructure};
            AddPackage(resourcePackage);
            Requests.Enqueue(resourcePackage, priority);
            return resourcePackage;
        }

        public void Update(float dt)
        {
            var packages = AllPackages.ToArray();
            foreach (var resourcePackage in packages)
            {
                if (resourcePackage.ResourceRequestState != ResourceRequestState.OnItsWay)
                    continue;
                resourcePackage.Update(dt);
            }
        }

        public void UpdateProvisionPriority(ResourcePackage resourcePackage, RequestPriority priority)
        {
            var provisions = Provisions.ToArray();
            foreach (var provision in provisions)
            {
                if (provision == resourcePackage)
                {
                    Provisions.UpdatePriority(provision, priority);
                }
            }
        }

        public void UpdateRequestPriority(ResourcePackage resourcePackage, RequestPriority priority)
        {
            var requests = Requests.ToArray();
            foreach (var request in requests)
            {
                if (request == resourcePackage)
                {
                    Requests.UpdatePriority(request, priority);
                }
            }
        }

        private void AddPackage(ResourcePackage resourcePackage)
        {
            AllPackageList.Add(resourcePackage);
            resourcePackage.StartedMoving += ResourcePackage_StartedMoving;
            resourcePackage.ArrivedAtDestination += ResourcePackage_ArrivedAtDestination;
        }
        private bool HasPathFilter(Structure startStructure, Structure destinationStructure)
        {
            return World.PathFinder.GetPath(startStructure.Position, destinationStructure.Position) != null;
        }

        private void ResourcePackage_ArrivedAtDestination(ResourcePackage resourcePackage)
        {
            PackageArrived?.Invoke(this, resourcePackage);
            AllPackageList.Remove(resourcePackage);
        }

        private void ResourcePackage_StartedMoving(ResourcePackage resourcePackage)
        {
            PackageStarted?.Invoke(this, resourcePackage);
        }
    }
}