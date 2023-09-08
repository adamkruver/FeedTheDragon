using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Inventories.Signals
{
    public class CreateInventorySignal : ISignal
    {
        public CreateInventorySignal(int ownerId, int capacity)
        {
            OwnerId = ownerId;
            Capacity = capacity;
        }

        public int OwnerId { get; }
        public int Capacity { get; }
    }
}