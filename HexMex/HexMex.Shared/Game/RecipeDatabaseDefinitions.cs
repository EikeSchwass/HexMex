namespace HexMex.Game
{
    public partial class RecipeDatabase
    {
        private void CreateRecipes()
        {
            // Village
            Recipes.Add(typeof(VillageBuilding), new Recipe(new[]
            {
                new ResourceIngredient(3, typeof(WaterResource)),
                new ResourceIngredient(2, typeof(NutritionResource)),

            }, new[]
            {
                new ResourceIngredient(5, typeof(WorkforceResource))
            }, 30));
        }
    }
}