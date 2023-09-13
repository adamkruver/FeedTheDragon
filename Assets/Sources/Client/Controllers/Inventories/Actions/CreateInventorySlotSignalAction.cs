using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Inventories.Slots.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class CreateInventorySlotSignalAction : ISignalAction<CreateInventorySlotSignal>
    {
        private readonly CreateInventorySlotQuery _createInventorySlotQuery;

        public CreateInventorySlotSignalAction(
            CreateInventorySlotQuery createInventorySlotQuery
        )
        {
            _createInventorySlotQuery = createInventorySlotQuery;
        }

        public void Handle(CreateInventorySlotSignal signal)
        {
            _createInventorySlotQuery.Handle(signal.InventoryId);
        }
    }
}