using System;
using System.Collections.Generic;

namespace HexMex.Game.Buildings
{
    public abstract class Structure : IRenderable<Structure>
    {
        protected Structure(HexagonNode position, ResourceManager resourceManager)
        {
            Position = position;
            ResourceManager = resourceManager;
            RenderInformation = new StructureRenderInformation(this);
        }

        public abstract event Action<Structure> RequiresRedraw;

        public HexagonNode Position { get; }
        public StructureRenderInformation RenderInformation { get; }
        public IReadOnlyCollection<ResourceProvision> ResourceProvisions => ResourceProvisionList.AsReadOnly();

        public IReadOnlyCollection<ResourceRequest> ResourceRequests => ResourceRequestList.AsReadOnly();

        private ResourceManager ResourceManager { get; }
        private List<ResourceProvision> ResourceProvisionList { get; } = new List<ResourceProvision>();
        private List<ResourceRequest> ResourceRequestList { get; } = new List<ResourceRequest>();

        public abstract void OnRequiresRedraw();

        /// <summary>
        /// Get's called everytime a Resource arrives which destination was this Building.
        /// </summary>
        /// <param name="resource">The Resource that arrived.</param>
        public virtual void OnResourceArrived(ResourcePackage resource)
        {
        }

        /// <summary>
        /// Get's called everytime a Resource passes the Node the Building is located at.
        /// </summary>
        /// <param name="resource">The Resource that passes through.</param>
        public virtual void OnResourcePassesThrough(ResourcePackage resource)
        {
        }

        protected void ProvideResource(ResourceType resourceType, RequestPriority priority = RequestPriority.Normal)
        {
            var resourceProvision = ResourceManager.ProvideResource(resourceType, this, priority);
            ResourceProvisionList.Add(resourceProvision);
        }

        protected void RequestResource(ResourceType resourceType, RequestPriority requestPriority = RequestPriority.Normal)
        {
            var requestResource = ResourceManager.RequestResource(resourceType, this, requestPriority);
            ResourceRequestList.Add(requestResource);
        }
    }
}