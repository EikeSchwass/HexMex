namespace HexMex.Game {
    public class ConstructionInformation : ResourceCollection
    {
        public float ConstructionTime { get; }
        public ConstructionInformation(float constructionTime, EnvironmentResource environmentResource, params ResourceTypeSource[] resourceTypes) : base(environmentResource, resourceTypes)
        {
            ConstructionTime = constructionTime;
        }
        public ConstructionInformation(float constructionTime, params ResourceTypeSource[] resourceTypes) : this(constructionTime, (EnvironmentResource)0, resourceTypes) { }
    }
}