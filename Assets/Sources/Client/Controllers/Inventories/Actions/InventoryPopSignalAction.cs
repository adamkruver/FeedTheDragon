using System;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class InventoryPopSignalAction : ISignalAction<PopInventorySignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly InventoryPopItemQuery _inventoryPopItemQuery;
        private readonly GetInventoryIdQuery _getInventoryIdQuery;

        public InventoryPopSignalAction
        (
            ICurrentPlayerService currentPlayerService,
            InventoryPopItemQuery inventoryPopItemQuery,
            GetInventoryIdQuery getInventoryIdQuery
        )
        {
            _currentPlayerService = currentPlayerService;
            _inventoryPopItemQuery = inventoryPopItemQuery;
            _getInventoryIdQuery = getInventoryIdQuery;
        }

        public void Handle(PopInventorySignal signal)
        {
            int inventoryId = _getInventoryIdQuery.Handle(_currentPlayerService.CharacterId);
            int itemId = _inventoryPopItemQuery.Handle(inventoryId);
        }
    }
}