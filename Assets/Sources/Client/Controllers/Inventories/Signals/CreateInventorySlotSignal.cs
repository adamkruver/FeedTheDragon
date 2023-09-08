using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Inventories.Signals
{
    public class CreateInventorySlotSignal : ISignal
    {
        public CreateInventorySlotSignal(int inventoryId) => 
            InventoryId = inventoryId;

        public int InventoryId { get; }
    }
}