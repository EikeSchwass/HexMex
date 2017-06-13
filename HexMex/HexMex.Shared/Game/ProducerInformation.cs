namespace HexMex.Game {
    public class ProducerInformation
    {
        public IngredientsCollection Ingredients { get; }
        public float ProductionTime { get; }
        public ProductsCollection Products { get; }

        public ProducerInformation(IngredientsCollection ingredients, ProductsCollection products, float productionTime)
        {
            Ingredients = ingredients;
            Products = products;
            ProductionTime = productionTime;
        }
    }
}