using System.Collections.Generic;
using System.Linq;

namespace HexMex.Game
{
    public class Recipe
    {
        public float Duration { get; }

        public Recipe(ICollection<ResourceIngredient> resourceIngredients, ICollection<ResourceIngredient> resourceOutputs, float duration)
        {
            Ingredients = resourceIngredients.ToList();
            Outputs = resourceOutputs.ToList();
            Duration = duration;
        }

        private List<ResourceIngredient> Ingredients { get; }
        private List<ResourceIngredient> Outputs { get; }

        public IReadOnlyCollection<ResourceIngredient> GetAllIngredients()
        {
            return Ingredients.ToList().AsReadOnly();
        }
    }
}