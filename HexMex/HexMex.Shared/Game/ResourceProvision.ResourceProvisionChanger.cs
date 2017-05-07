namespace HexMex.Game
{
    public partial class ResourceProvision
    {
        public class ResourceProvisionChanger
        {
            protected ResourceProvisionChanger(ResourceProvision parent)
            {
                ResourceProvision = parent;
            }

            public static void SetFactory()
            {
                ResourceProvisionChangerFactory = mr => new ResourceProvisionChanger(mr);
            }

            public ResourceProvision ResourceProvision { get; }

            public void SetRequestState(ResourceRequestState requestState)
            {
                ResourceProvision.RequestState = requestState;
            }
        }
    }
}