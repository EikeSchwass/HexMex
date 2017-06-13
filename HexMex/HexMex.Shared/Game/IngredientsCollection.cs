namespace HexMex.Game {
    public class IngredientsCollection : ResourceCollection
    {
        public IngredientsCollection(EnvironmentResource environmentResource, params ResourceTypeSource[] resourceTypes) : base(environmentResource, resourceTypes) { }
        public IngredientsCollection(params ResourceTypeSource[] resourceTypes) : this((EnvironmentResource)0, resourceTypes) { }
    }
}