using System;
using System.Collections.Generic;
using System.Globalization;

namespace HexMex.Helper
{
    public abstract class DataLoader
    {
        protected abstract string this[string key] { get; }
        private Dictionary<string, object> Cache { get; } = new Dictionary<string, object>();

        public T GetValue<T>(string key)
        {
            if (Cache.ContainsKey(key))
                return (T)Cache[key];
            var value = this[key];

            /*
             *  switch (typeof(T))
             *  {
             *      case var t when t == typeof(Recipe):
             *          Cache.Add(key, LoadRecipe(key));
             *          return (T)Cache[key];
             *  }
             */

            var changedType = Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            Cache.Add(key, changedType);
            return (T)Cache[key];
        }

        public T GetValue<T>(int key) => GetValue<T>(key.ToString());

        /*
        private Recipe LoadRecipe(string key)
        {
            var recipeData = this[key];
            var categories = recipeData.Split('|');
            var ingredients = categories[0].Split(',');
            var outputs = categories[1].Split(',');
            float duration = Convert.ToSingle(categories[2]);

            List<ResourceIngredient> resourceIngredients = (from ingredient in ingredients
                                                            let amount = ingredient.Contains("@") ? Convert.ToInt32(ingredient.Split('@')[1]) : 1
                                                            let typeString = ingredient.Split('@')[0]
                                                            let type = Type.GetType(typeString + "Resource")
                                                            select new ResourceIngredient(amount, type)).ToList();

            List<ResourceIngredient> resourceOutputs = (from output in outputs
                                                        let amount = output.Contains("@") ? Convert.ToInt32(output.Split('@')[1]) : 1
                                                        let typeString = output.Split('@')[0]
                                                        let type = Type.GetType(typeString + "Resource")
                                                        select new ResourceIngredient(amount, type)).ToList();
            return new Recipe(resourceIngredients, resourceOutputs, duration);
        }
        */
    }
}