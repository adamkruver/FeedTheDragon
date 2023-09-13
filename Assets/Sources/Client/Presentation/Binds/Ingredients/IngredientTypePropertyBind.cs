using System;
using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.Presentation.Views.Inventories;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.Ingredients
{
    public class IngredientTypePropertyBind : BindableViewProperty<Type>, IIngredientTypePropertyBind
    {
        [SerializeField] private SlotView _view;

        public override Type BindableProperty
        {
            get => null;
            set => _view.Show(value);
        }
    }
}