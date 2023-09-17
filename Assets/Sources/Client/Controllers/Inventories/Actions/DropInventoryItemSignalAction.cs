using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Inventories.Commands;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class DropInventoryItemSignalAction : ISignalAction<DropInventoryItemSignal>
    {
        private readonly DropInventoryItemCommand _dropInventoryItemCommand;

        public DropInventoryItemSignalAction(DropInventoryItemCommand dropInventoryItemCommand) =>
            _dropInventoryItemCommand = dropInventoryItemCommand;

        public void Handle(DropInventoryItemSignal signal) =>
            _dropInventoryItemCommand.Handle(signal.InventorySlotId);
    }
}