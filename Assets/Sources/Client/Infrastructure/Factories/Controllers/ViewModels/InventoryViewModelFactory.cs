using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Inventories;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class InventoryViewModelFactory : IViewModelFactory<InventoryViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly InventorySlotObserverViewModelComponentFactory _inventorySlotObserverViewModelComponentFactory;
        private readonly IBindableViewBuilder<InventorySlotViewModel> _slotViewBuilder;

        public InventoryViewModelFactory(
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            InventorySlotObserverViewModelComponentFactory inventorySlotObserverViewModelComponentFactory 
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _inventorySlotObserverViewModelComponentFactory = inventorySlotObserverViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new InventoryViewModel(
                new[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                    _inventorySlotObserverViewModelComponentFactory.Create(id)
                }
            );
        }
    }
}