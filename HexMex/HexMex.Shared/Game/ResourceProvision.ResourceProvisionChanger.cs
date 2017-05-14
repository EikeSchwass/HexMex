namespace HexMex.Game
{
    public partial class ResourceProvision
    {
        public class ResourceProvisionChanger
        {
            public ResourceProvision ResourceProvision { get; }

            protected ResourceProvisionChanger(ResourceProvision parent)
            {
                ResourceProvision = parent;
            }

            public static void SetFactory()
            {
                ResourceProvisionChangerFactory = mr => new ResourceProvisionChanger(mr);
            }

            public void SetRequestState(ResourceRequestState requestState)
            {
                ResourceProvision.RequestState = requestState;
            }
        }
    }
}