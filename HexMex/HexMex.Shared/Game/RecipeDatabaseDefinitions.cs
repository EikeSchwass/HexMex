namespace HexMex.Game
{
    public partial class RecipeDatabase
    {
        private void CreateRecipes()
        {
            // Village
            Recipes.Add(typeof(VillageBuilding), new Recipe(new[]
            {
                new ResourceIngredient(3, ResourceType.Water),
                new ResourceIngredient(2, ResourceType.Nutrition),

            }, new[]
            {
                new ResourceIngredient(5, ResourceType.Workforce)
            }, 30));
        }
    }
}