namespace HexMex.Game
{
    public class ProductsCollection : ResourceCollection
    {
        public Knowledge Knowledge { get; }

        public ProductsCollection(EnvironmentResource environmentResource, Knowledge knowledge, params ResourceTypeSource[] resourceTypes) : base(environmentResource, resourceTypes)
        {
            Knowledge = knowledge;
        }
        public ProductsCollection(EnvironmentResource environmentResource, params ResourceTypeSource[] resourceTypes) : this(environmentResource, Knowledge.Zero, resourceTypes) { }
        public ProductsCollection(params ResourceTypeSource[] resourceTypes) : this((EnvironmentResource)0, resourceTypes) { }
    }
}