using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Presentation.Views;
using Sources.Client.Presentation.Views.Inventories;
using Sources.Client.PresentationInterfaces.Views;

namespace Sources.Client.Infrastructure.Factories.Presentation.Views
{
    public class InventorySlotViewFactory : ViewFactoryBase<InventorySlotView>, IInventorySlotViewFactory
    {
        private static readonly string s_inventoryCellViewPath = "Views/Inventory/";

        private readonly IngredientViewFactory _ingredientViewFactory;
        private readonly IIngredientType[] _availableTypes;
        
        public InventorySlotViewFactory(
            IBindableViewFactory bindableViewFactory,
            IngredientViewFactory ingredientViewFactory,
            IIngredientType[] availableTypes) : base(bindableViewFactory, s_inventoryCellViewPath)
        {
            _ingredientViewFactory = ingredientViewFactory;
            _availableTypes = availableTypes;
        }

        public IInventorySlotView Create()
        {
            InventorySlotView slotView = Create(nameof(InventorySlotView));

            foreach (IIngredientType ingredientType in _availableTypes)
                slotView.Add(ingredientType, _ingredientViewFactory.Create(ingredientType.GetType()));
            
            slotView.Hide();

            return slotView;
        }
    }
}