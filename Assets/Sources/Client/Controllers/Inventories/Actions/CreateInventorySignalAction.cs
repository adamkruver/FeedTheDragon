using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.ViewProviders;
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
        private readonly IInventoryViewFactory _inventoryViewFactory;
        private readonly InventoryViewModelFactory _inventoryViewModelFactory;
        private readonly ViewProvider _viewProvider;
        private readonly CreateInventoryQuery _createInventoryQuery;

        public CreateInventorySignalAction(
            ISignalBus signalBus,
            IInventoryViewFactory inventoryViewFactory,
            InventoryViewModelFactory inventoryViewModelFactory,
            ViewProvider viewProvider,
            CreateInventoryQuery createInventoryQuery
        )
        {
            _signalBus = signalBus;
            _inventoryViewFactory = inventoryViewFactory;
            _inventoryViewModelFactory = inventoryViewModelFactory;
            _viewProvider = viewProvider;
            _createInventoryQuery = createInventoryQuery;
        }

        public void Handle(CreateInventorySignal signal)
        {
            int inventoryId = _createInventoryQuery.Handle(signal.OwnerId);
            IViewModel viewModel = _inventoryViewModelFactory.Create(inventoryId);
            IInventoryView view = _inventoryViewFactory.Create();

            _viewProvider.InventoryView = view;

            view.Bind(viewModel);

            CreateInventoryCells(inventoryId, signal.Capacity);
        }

        private void CreateInventoryCells(int inventoryId, int capacity)
        {
            for (int i = 0; i < capacity; i++)
                _signalBus.Handle(new CreateInventorySlotSignal(inventoryId));
        }
    }
}