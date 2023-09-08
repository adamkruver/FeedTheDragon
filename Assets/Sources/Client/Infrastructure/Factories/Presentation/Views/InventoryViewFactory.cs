using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Presentation.Views;
using Sources.Client.Presentation.Views.Inventories;
using Sources.Client.PresentationInterfaces.Views;

namespace Sources.Client.Infrastructure.Factories.Presentation.Views
{
    public class InventoryViewFactory : IInventoryViewFactory
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private static readonly string s_inventoryPrefabPath = "Views/Inventory/";

        public InventoryViewFactory(IBindableViewFactory bindableViewFactory) => 
            _bindableViewFactory = bindableViewFactory;

        public IInventoryView Create()
        {
            IBindableView bindableView = _bindableViewFactory.Create(s_inventoryPrefabPath, nameof(InventoryView));

            return (InventoryView)bindableView;
        }
    }
}