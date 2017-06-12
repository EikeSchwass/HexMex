using System.Collections.ObjectModel;

namespace HexMex.Game
{
    public class StructureDescription
    {
        public ResourceCollection ConstructionCost { get; }
        public float ConstructionTime { get; }
        public string Description { get; }
        public bool IsProducer => ProductionInformation != null;
        public string Name { get; }
        public ProducerInformation ProductionInformation { get; }

        public StructureDescription(string name, string description, ResourceCollection constructionCost, float constructionTime, ResourceCollection ingredients, ResourceCollection products, float productionTime)
        {
            ConstructionTime = constructionTime;
            ProducerInformation productionInformation = null;
            if (ingredients != null || products != null)
            {
                productionInformation = new ProducerInformation(ingredients, products, productionTime);
            }
            ProductionInformation = productionInformation;
            ConstructionCost = constructionCost;
            Name = name;
            Description = description;
        }

        public StructureDescription(string name, string description, ResourceCollection constructionCost, float constructionTime) : this(name, description, constructionCost, constructionTime, null, null, 0) { }

        public class ProducerInformation
        {
            public ResourceCollection Ingredients { get; }
            public float ProductionTime { get; }
            public ResourceCollection Products { get; }

            public ProducerInformation(ResourceCollection ingredients, ResourceCollection products, float productionTime)
            {
                Ingredients = ingredients;
                Products = products;
                ProductionTime = productionTime;
            }
        }

        public class ResourceCollection
        {
            public EnvironmentResource EnvironmentResource { get; }
            public ReadOnlyCollection<ResourceType> ResourceTypes { get; }

            public ResourceCollection(EnvironmentResource environmentResource, params ResourceType[] resourceTypes)
            {
                EnvironmentResource = environmentResource;
                ResourceTypes = new ReadOnlyCollection<ResourceType>(resourceTypes);
            }

            public ResourceCollection(params ResourceType[] resourceTypes) : this((EnvironmentResource)0, resourceTypes) { }
        }
    }
}