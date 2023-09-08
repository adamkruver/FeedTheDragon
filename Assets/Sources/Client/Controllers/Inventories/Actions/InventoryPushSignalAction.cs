using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using Sources.Client.UseCases.Inventories.Commands;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class InventoryPushSignalAction : ISignalAction<PushIngredientToInventorySignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly InventoryPushItemCommand _inventoryPushItemCommand;
        private readonly GetInventoryIdQuery _getInventoryIdQuery;
        private readonly HideCommand _hideCommand;

        public InventoryPushSignalAction(
            ICurrentPlayerService currentPlayerService,
            InventoryPushItemCommand inventoryPushItemCommand,
            GetInventoryIdQuery getInventoryIdQuery,
            HideCommand hideCommand
        )
        {
            _currentPlayerService = currentPlayerService;
            _inventoryPushItemCommand = inventoryPushItemCommand;
            _getInventoryIdQuery = getInventoryIdQuery;
            _hideCommand = hideCommand;
        }

        public void Handle(PushIngredientToInventorySignal signal)
        {
            int inventoryId = _getInventoryIdQuery.Handle(_currentPlayerService.CharacterId);

            _inventoryPushItemCommand.Handle(inventoryId, signal.IngredientId);
            _hideCommand.Handle(signal.IngredientId);
        }
    }
}