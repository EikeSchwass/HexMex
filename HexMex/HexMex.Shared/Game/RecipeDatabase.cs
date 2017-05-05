using System;
using System.Collections.Generic;

namespace HexMex.Game
{
    public partial class RecipeDatabase
    {
        private Dictionary<Type, Recipe> Recipes { get; } = new Dictionary<Type, Recipe>();

        public Recipe this[Type buildingType]
        {
            get
            {
                var currentType = buildingType;
                while (currentType != null && !Recipes.ContainsKey(currentType))
                    currentType = currentType.BaseType;
                if (currentType == null)
                    throw new KeyNotFoundException($"No Recipe for {buildingType} found");
                return Recipes[currentType];
            }
        }

        public RecipeDatabase()
        {
            CreateRecipes();
        }

    }
}