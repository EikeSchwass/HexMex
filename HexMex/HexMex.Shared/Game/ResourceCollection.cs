using System.Collections.ObjectModel;

namespace HexMex.Game
{
    public abstract class ResourceCollection
    {
        public EnvironmentResource EnvironmentResource { get; }
        public ReadOnlyCollection<ResourceTypeSource> ResourceTypes { get; }

        protected ResourceCollection(EnvironmentResource environmentResource, params ResourceTypeSource[] resourceTypes)
        {
            EnvironmentResource = environmentResource;
            ResourceTypes = new ReadOnlyCollection<ResourceTypeSource>(resourceTypes);
        }

        protected ResourceCollection(params ResourceTypeSource[] resourceTypes) : this((EnvironmentResource)0, resourceTypes) { }
    }
}