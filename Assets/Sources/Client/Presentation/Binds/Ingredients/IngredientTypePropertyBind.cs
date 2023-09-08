using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Presentation.Views.Inventories;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.Ingredients
{
    public class IngredientTypePropertyBind : BindableViewProperty<IIngredientType>, IIngredientTypePropertyBind
    {
        [SerializeField] private InventorySlotView _view;

        public override IIngredientType BindableProperty
        {
            get { return null; }
            set { _view.Show(value); }
        }
    }
}