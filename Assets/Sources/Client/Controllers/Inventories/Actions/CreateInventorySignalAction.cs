using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Domain.Inventories;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.ViewProviders;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Presentation.Views;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.PresentationInterfaces.Views;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class CreateInventorySignalAction : ISignalAction<CreateInventorySignal>
    {
        private readonly ISignalBus _signalBus;
        private readonly IBindableViewBuilder<InventoryViewModel> _viewBuilder;
        private readonly ViewProvider _viewProvider;
        private readonly CreateInventoryQuery _createInventoryQuery;

        public CreateInventorySignalAction(
            ISignalBus signalBus,
            IBindableViewBuilder<InventoryViewModel> viewBuilder,
            ViewProvider viewProvider,
            CreateInventoryQuery createInventoryQuery
        )
        {
            _signalBus = signalBus;
            _viewBuilder = viewBuilder;
            _viewProvider = viewProvider;
            _createInventoryQuery = createInventoryQuery;
        }

        public void Handle(CreateInventorySignal signal)
        {
            int inventoryId = _createInventoryQuery.Handle(signal.OwnerId);
            IBindableView view = _viewBuilder.Build(inventoryId, nameof(Inventory));

            _viewProvider.InventoryView = (IInventoryView)view;

            CreateInventoryCells(inventoryId, signal.Capacity);
        }

        private void CreateInventoryCells(int inventoryId, int capacity)
        {
            for (int i = 0; i < capacity; i++)
                _signalBus.Handle(new CreateInventorySlotSignal(inventoryId));
        }
    }
}