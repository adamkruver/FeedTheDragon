using System;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.InventoryComponents.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class InventoryPopSignalAction : ISignalAction<PopInventorySignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly IEntityRepository _entityRepository;
        private readonly TryPopIngredientFromInventoryQuery _tryPopIngredientFromInventoryQuery;

        public InventoryPopSignalAction
        (
            ICurrentPlayerService currentPlayerService,
            IEntityRepository entityRepository,
            TryPopIngredientFromInventoryQuery tryPopIngredientFromInventoryQuery
        )
        {
            _currentPlayerService = currentPlayerService;
            _entityRepository = entityRepository;
            _tryPopIngredientFromInventoryQuery = tryPopIngredientFromInventoryQuery;
        }

        public void Handle(PopInventorySignal signal)
        {
            _tryPopIngredientFromInventoryQuery.Handle(_currentPlayerService.CharacterId, out Ingredient _);
            
            throw new NotImplementedException();
        }
    }
}