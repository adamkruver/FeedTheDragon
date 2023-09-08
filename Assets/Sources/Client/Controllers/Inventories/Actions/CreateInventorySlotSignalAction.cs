using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Presentation.Views;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.InfrastructureInterfaces.ViewProviders;
using Sources.Client.PresentationInterfaces.Views;
using Sources.Client.UseCases.Inventories.Slots.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class CreateInventorySlotSignalAction : ISignalAction<CreateInventorySlotSignal>
    {
        private readonly IInventoryViewProvider _inventoryViewProvider;
        private readonly IInventorySlotViewFactory _inventorySlotViewFactory;
        private readonly InventorySlotViewModelFactory _inventorySlotViewModelFactory;
        private readonly CreateInventorySlotQuery _createInventorySlotQuery;

        public CreateInventorySlotSignalAction(
            IInventoryViewProvider inventoryViewProvider,
            IInventorySlotViewFactory inventorySlotViewFactory,
            InventorySlotViewModelFactory inventorySlotViewModelFactory,
            CreateInventorySlotQuery createInventorySlotQuery
            )
        {
            _inventoryViewProvider = inventoryViewProvider;
            _inventorySlotViewFactory = inventorySlotViewFactory;
            _inventorySlotViewModelFactory = inventorySlotViewModelFactory;
            _createInventorySlotQuery = createInventorySlotQuery;
        }

        public void Handle(CreateInventorySlotSignal signal)
        {
            int id = _createInventorySlotQuery.Handle(signal.InventoryId);

            IViewModel viewModel = _inventorySlotViewModelFactory.Create(id);
            IInventorySlotView view = _inventorySlotViewFactory.Create();
            
            _inventoryViewProvider.InventoryView.Add(view);
            
            view.Bind(viewModel);
        }
    }
}