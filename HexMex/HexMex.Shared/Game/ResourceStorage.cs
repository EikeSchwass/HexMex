using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HexMex.Game.Buildings
{
    public class ResourceStorage : IEnumerable<ResourcePackage>
    {
        public int Count => StoredResources.Count;
        private List<ResourcePackage> StoredResources { get; } = new List<ResourcePackage>();

        public IEnumerator<ResourcePackage> GetEnumerator()
        {
            return StoredResources.GetEnumerator();
        }

        public IEnumerable<ResourcePackage> GetResourcesOfType(ResourceType resourceType)
        {
            return StoredResources.Where(r => r.ResourceType == resourceType);
        }

        public bool HasEnoughStored(IEnumerable<ResourceIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (StoredResources.Count(s => s.ResourceType == ingredient.ResourceType) < ingredient.Amount)
                    return false;
            }
            return true;
        }

        public ResourcePackage RemoveResource(ResourcePackage resource)
        {
            StoredResources.Remove(resource);
            return resource;
        }

        public ResourcePackage RemoveResourceOfType(ResourceType resourceType)
        {
            var resource = StoredResources.Last(r => r.ResourceType == resourceType);
            RemoveResource(resource);
            return resource;
        }

        public void RemoveResources(ResourceIngredient[] ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                for (int i = 0; i < ingredient.Amount; i++)
                {
                    var resource = StoredResources.First(r => r.ResourceType == ingredient.ResourceType);
                    RemoveResource(resource);
                }
            }
        }

        public void StoreResource(ResourcePackage resource)
        {
            StoredResources.Add(resource);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}