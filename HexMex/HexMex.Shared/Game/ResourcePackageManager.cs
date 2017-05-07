using System;
using System.Collections;
using System.Collections.Generic;
using CocosSharp;

namespace HexMex.Game
{
    public class ResourcePackageManager : ICollection<ResourcePackage>, ICCUpdatable
    {

        public int Count => ResourcePackages.Count;
        public bool IsReadOnly { get; } = false;

        public ResourcePackage this[int index]
        {
            get => ResourcePackages[index];
            set => ResourcePackages[index] = value;
        }

        private List<ResourcePackage> ResourcePackages { get; } = new List<ResourcePackage>();

        public void Add(ResourcePackage item)
        {
            ResourcePackages.Add(item);
            item.ArrivedAtDestination += ResourcePackageArrivedAtDestination;
            PackageAdded?.Invoke(this, item);
        }

        public void Clear()
        {
            throw new NotSupportedException("Why is this called?");
        }

        public bool Contains(ResourcePackage item)
        {
            return ResourcePackages.Contains(item);
        }

        public void CopyTo(ResourcePackage[] array, int arrayIndex)
        {
            ResourcePackages.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ResourcePackage> GetEnumerator()
        {
            return ResourcePackages.GetEnumerator();
        }

        public bool Remove(ResourcePackage item)
        {
            throw new NotSupportedException("This will be handled automatically");
        }

        public void Update(float dt)
        {
            foreach (var resourcePackage in ResourcePackages.ToArray())
            {
                resourcePackage.Update(dt);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ResourcePackageArrivedAtDestination(ResourcePackage resourcePackage)
        {
            ResourcePackages.Remove(resourcePackage);
            resourcePackage.ArrivedAtDestination -= ResourcePackageArrivedAtDestination;
            PackageRemoved?.Invoke(this, resourcePackage);
        }

        public event Action<ResourcePackageManager, ResourcePackage> PackageAdded;
        public event Action<ResourcePackageManager, ResourcePackage> PackageRemoved;
    }
}