namespace HexMex.Game
{
    public class Cunstruction<T> : Structure where T : Building
    {
        public Cunstruction(HexagonCornerPosition position, ResourceManager resourceManager, params ResourceIngredient[] ingredients) : base(position)
        {
            ResourceManager = resourceManager;
            Ingredients = ingredients;
            foreach (var ingredient in ingredients)
            {
                var resourceType = ingredient.ResourceType;
                for (int i = 0; i < ingredient.Amount; i++)
                {
                    ResourceManager.RequestResource(resourceType, this, 0);
                }
            }
        }

        private ResourceIngredient[] Ingredients { get; }
        private ResourceManager ResourceManager { get; }
    }
}