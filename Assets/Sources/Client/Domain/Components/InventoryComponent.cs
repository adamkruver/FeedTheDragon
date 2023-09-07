using System.Collections.Generic;
using Sources.Client.Domain.Ingredients;
using UnityEngine;

namespace Sources.Client.Domain.Components
{
    public class InventoryComponent : IComponent
    {
        private readonly int _maxCount;
        private readonly List<Ingredient> _ingredients = new List<Ingredient>();

        public InventoryComponent(int maxCount)
        {
            _maxCount = maxCount;
        }

        public bool IsEmpty => _ingredients.Count == 0;

        public bool CanPush => _ingredients.Count < _maxCount;

        public bool TryPush(Ingredient ingredient)
        {
            if (CanPush == false)
                return false;

            _ingredients.Add(ingredient);

            Debug.Log("Pushed ingredient to inventory " + ingredient.GetType().Name);
            return true;
        }

        public bool TryPop(out Ingredient ingredient)
        {
            ingredient = null;

            if (IsEmpty)
                return false;

            ingredient = _ingredients[0];
            _ingredients.Remove(ingredient);

            return true;
        }
    }
}