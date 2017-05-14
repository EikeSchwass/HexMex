namespace HexMex.Game
{
    public partial class ResourceRequest
    {
        public class ResourceRequestChanger
        {
            public ResourceRequest ResourceRequest { get; }

            private ResourceRequestChanger(ResourceRequest parent)
            {
                ResourceRequest = parent;
            }

            public static void SetFactory()
            {
                ResourceRequestChangerFactory = mr => new ResourceRequestChanger(mr);
            }

            public void SetRequestState(ResourceRequestState requestState)
            {
                ResourceRequest.RequestState = requestState;
            }
        }
    }
}