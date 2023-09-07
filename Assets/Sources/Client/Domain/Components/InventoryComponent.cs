using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Ingredients;

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
        
        public IEnumerable<IIngredientType> IngredientTypes => _ingredients.Select(ingredient => ingredient.Type);

        public event Action Changed;

        public bool TryPush(Ingredient ingredient)
        {
            if (CanPush == false)
                return false;

            _ingredients.Add(ingredient);
            Changed?.Invoke();
            
            return true;
        }

        public bool TryPop(out Ingredient ingredient)
        {
            ingredient = null;

            if (IsEmpty)
                return false;

            ingredient = _ingredients[0];
            _ingredients.Remove(ingredient);
            Changed?.Invoke();

            return true;
        }
    }
}