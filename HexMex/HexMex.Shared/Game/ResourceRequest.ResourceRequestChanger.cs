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
            public ResourceRequest ResourceRequest { get; }
            
            public void SetRequestState(ResourceRequestState requestState)
            {
                ResourceRequest.RequestState = requestState;
            }

            public static void SetFactory()
            {
                ResourceRequestChangerFactory = mr => new ResourceRequestChanger(mr);
            }
        }
    }
}