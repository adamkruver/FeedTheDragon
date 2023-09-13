using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class CreateInventorySignalAction : ISignalAction<CreateInventorySignal>
    {
        private readonly ISignalBus _signalBus;
        private readonly IBindableViewBuilder<InventoryViewModel> _viewBuilder;
        private readonly CreateInventoryQuery _createInventoryQuery;

        public CreateInventorySignalAction(
            ISignalBus signalBus,
            IBindableViewBuilder<InventoryViewModel> viewBuilder,
            CreateInventoryQuery createInventoryQuery
        )
        {
            _signalBus = signalBus;
            _viewBuilder = viewBuilder;
            _createInventoryQuery = createInventoryQuery;
        }

        public void Handle(CreateInventorySignal signal)
        {
            int inventoryId = _createInventoryQuery.Handle(signal.OwnerId);
            _viewBuilder.Build(inventoryId, nameof(Inventory));

            CreateInventoryCells(inventoryId, signal.Capacity);
        }

        private void CreateInventoryCells(int inventoryId, int capacity)
        {
            for (int i = 0; i < capacity; i++)
                _signalBus.Handle(new CreateInventorySlotSignal(inventoryId));
        }
    }
}