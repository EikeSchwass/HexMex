namespace HexMex.Game
{
    public partial class ResourceRequest
    {
        public class ResourceRequestChanger
        {
            protected ResourceRequestChanger(ResourceRequest parent)
            {
                ResourceRequest = parent;
            }

            static ResourceRequestChanger()
            {
                ResourceRequestChangerFactory = mr => new ResourceRequestChanger(mr);
            }

            public ResourceRequest ResourceRequest { get; }

            public void SetResource(Resource resource)
            {
                ResourceRequest.Resource = resource;
            }

            public void SetRequestState(ResourceRequestState requestState)
            {
                ResourceRequest.RequestState = requestState;
            }
        }
    }
}