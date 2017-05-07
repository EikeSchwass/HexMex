namespace HexMex.Game
{
    public partial class ResourceRequest
    {
        public class ResourceRequestChanger
        {
            private ResourceRequestChanger(ResourceRequest parent)
            {
                ResourceRequest = parent;
            }

            static ResourceRequestChanger()
            {
                ResourceRequestChangerFactory = mr => new ResourceRequestChanger(mr);
            }

            public ResourceRequest ResourceRequest { get; }
            
            public void SetRequestState(ResourceRequestState requestState)
            {
                ResourceRequest.RequestState = requestState;
            }
        }
    }
}