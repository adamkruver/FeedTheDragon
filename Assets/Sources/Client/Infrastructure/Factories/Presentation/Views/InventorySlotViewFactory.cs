using System;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Presentation.Views.Inventories;
using Sources.Client.PresentationInterfaces.Views;

namespace Sources.Client.Infrastructure.Factories.Presentation.Views
{
    public class InventorySlotViewFactory : IBindableViewFactory
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly IngredientViewFactory _ingredientViewFactory;
        private readonly IIngredientType[] _availableTypes;

        public InventorySlotViewFactory(
            IBindableViewFactory bindableViewFactory,
            IngredientViewFactory ingredientViewFactory,
            IIngredientType[] availableTypes
        )
        {
            _bindableViewFactory = bindableViewFactory;
            _ingredientViewFactory = ingredientViewFactory;
            _availableTypes = availableTypes;
        }

        public IBindableView Create(string viewPath, string name, IBindableView parent = null)
        {
            IBindableView view = _bindableViewFactory.Create(viewPath, name);

            if (view is not InventorySlotView slotView)
                throw new InvalidCastException();
            
            foreach (IIngredientType ingredientType in _availableTypes)
                slotView.Add(ingredientType, _ingredientViewFactory.Create(ingredientType.GetType()));

            slotView.Hide();

            return slotView;
        }
    }
}