using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Inventories.Signals
{
    public class DropInventoryItemSignal : ISignal
    {
        public DropInventoryItemSignal(int inventorySlotId) =>
            InventorySlotId = inventorySlotId;

        public int InventorySlotId { get; }
    }
}