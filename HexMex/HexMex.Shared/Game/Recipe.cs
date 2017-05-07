using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HexMex.Game
{
    public class Recipe
    {
        public Recipe(IEnumerable<ResourceIngredient> ingredients, IEnumerable<ResourceIngredient> products, float duration)
        {
            Ingredients = new ReadOnlyCollection<ResourceIngredient>(ingredients.ToList());
            Outputs = new ReadOnlyCollection<ResourceIngredient>(products.ToList());
            Duration = duration;
        }

        public float Duration { get; }

        public IReadOnlyCollection<ResourceIngredient> Ingredients { get; }
        public IReadOnlyCollection<ResourceIngredient> Outputs { get; }
    }
}