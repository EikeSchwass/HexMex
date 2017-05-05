﻿using System;

namespace HexMex.Game
{
    public partial class ResourceRequest
    {
        private ResourceRequest(Type type, Structure structure, int priority = 0)
        {
            Type = type;
            Priority = priority;
            RequestingStructure = structure;
        }

        public Resource Resource { get; private set; }
        public int Priority { get; set; }
        public Structure RequestingStructure { get; }

        public ResourceRequestState RequestState { get; private set; }
        public Type Type { get; }

        private static Func<ResourceRequest, ResourceRequestChanger> ResourceRequestChangerFactory { get; set; }

        public static ResourceRequestChanger CreateResourceRequest(Type resourceType, Structure structure, int priority = 0)
        {
            var resourceRequest = new ResourceRequest(resourceType, structure, priority);
            var requestChanger = ResourceRequestChangerFactory(resourceRequest);
            return requestChanger;
        }
    }
}