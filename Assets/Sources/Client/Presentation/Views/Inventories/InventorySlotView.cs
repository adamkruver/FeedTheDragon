using System;
using System.Collections.Generic;
using Presentation.Frameworks.Mvvm.Views;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Presentation.Views.Ingredients;
using Sources.Client.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Inventories
{
    public class InventorySlotView : BindableView, IInventorySlotView
    {
        [SerializeField] private Transform _collectionParent;

        private readonly Dictionary<Type, IngredientView> _ingredients = new Dictionary<Type, IngredientView>();

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, false);

        public void Add(IIngredientType ingredientType, IngredientView ingredientView)
        {
            _ingredients[ingredientType.GetType()] = ingredientView;
            ingredientView.SetParent(_collectionParent);
        }

        public void Show(Type ingredient)
        {
            foreach (Type ingredientType in _ingredients.Keys)
            {
                if (ingredientType == ingredient)
                    _ingredients[ingredientType].Show();
                else
                    _ingredients[ingredientType].Hide();
            }
        }

        public void Hide()
        {
            foreach (IngredientView ingredientView in _ingredients.Values)
                ingredientView.Hide();
        }
    }
}