namespace HexMex.Game {
    public class ProductionInformation
    {
        public IngredientsCollection Ingredients { get; }
        public float ProductionTime { get; }
        public ProductsCollection Products { get; }

        public ProductionInformation(IngredientsCollection ingredients, ProductsCollection products, float productionTime)
        {
            Ingredients = ingredients;
            Products = products;
            ProductionTime = productionTime;
        }
    }
}