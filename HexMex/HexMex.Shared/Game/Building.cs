namespace HexMex.Game
{
    public abstract class Building : Structure
    {
        protected Building(HexagonCornerPosition position, ResourceManager resourceManager, RecipeDatabase recipeDatabase) : base(position)
        {
            ResourceManager = resourceManager;
            Recipe = recipeDatabase[GetType()];
        }

        public ResourceManager ResourceManager { get; }

        public Recipe Recipe { get; }
    }
}