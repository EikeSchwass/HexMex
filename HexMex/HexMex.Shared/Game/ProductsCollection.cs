namespace HexMex.Game {
    public class ProductsCollection : ResourceCollection
    {
        public ProductsCollection(EnvironmentResource environmentResource, params ResourceTypeSource[] resourceTypes) : base(environmentResource, resourceTypes) { }
        public ProductsCollection(params ResourceTypeSource[] resourceTypes) : this((EnvironmentResource)0, resourceTypes) { }
    }
}