using System;

namespace HexMex.Game.Buildings
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BuildingInformationAttribute : Attribute
    {
        public BuildingInformationAttribute(string name, string description, float constructionTime, params ResourceType[] constructionCost) : this(name, description, constructionTime, null, null, 0, constructionCost)
        {
        }

        public BuildingInformationAttribute(string name, string description, float constructionTime, ResourceType[] productionIngredients, ResourceType[] productionProducts, float productionTime, params ResourceType[] constructionCost)
        {
            ConstructionTime = constructionTime;
            ProducerInformation productionInformation = null;
            if (productionIngredients != null || productionProducts != null)
            {
                productionInformation = new ProducerInformation(productionIngredients, productionProducts, productionTime);
            }
            ProductionInformation = productionInformation;
            ConstructionCost = constructionCost;
            Name = name;
            Description = description;
        }

        public ResourceType[] ConstructionCost { get; }

        public float ConstructionTime { get; }
        public string Description { get; }
        public ProducerInformation ProductionInformation { get; }
        public bool IsProducer => ProductionInformation != null;
        public string Name { get; }

        public class ProducerInformation
        {
            public ProducerInformation(ResourceType[] ingredients, ResourceType[] products, float productionTime)
            {
                Ingredients = ingredients;
                Products = products;
                ProductionTime = productionTime;
            }

            public ResourceType[] Ingredients { get; }
            public float ProductionTime { get; }
            public ResourceType[] Products { get; }
        }
    }
}