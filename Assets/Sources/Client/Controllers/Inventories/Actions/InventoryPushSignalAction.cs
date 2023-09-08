using System;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Domain.Entities;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using Sources.Client.UseCases.InventoryComponents.Commands;
using Sources.Client.UseCases.InventoryComponents.Queries;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class InventoryPushSignalAction : ISignalAction<PushIngredientToInventorySignal>
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly CanPushInventoryQuery _canPushInventoryQuery;
        private readonly PushIngredientToInventoryCommand _pushIngredientToInventoryCommand;
        private readonly HideCommand _hideCommand;

        public InventoryPushSignalAction(
            IEntityRepository entityRepository,
            ICurrentPlayerService currentPlayerService,
            CanPushInventoryQuery canPushInventoryQuery,
            PushIngredientToInventoryCommand pushIngredientToInventoryCommand,
            HideCommand hideCommand
        )
        {
            _entityRepository = entityRepository;
            _currentPlayerService = currentPlayerService;
            _canPushInventoryQuery = canPushInventoryQuery;
            _pushIngredientToInventoryCommand = pushIngredientToInventoryCommand;
            _hideCommand = hideCommand;
        }

        public void Handle(PushIngredientToInventorySignal signal)
        {
            int inventoryId = _currentPlayerService.CharacterId; //todo: Get from valid source;

            if (_canPushInventoryQuery.Handle(inventoryId) == false)
                return;

            IEntity ingredientEntity = _entityRepository.Get(signal.IngredientId);

            if (ingredientEntity is not Ingredient ingredient) //todo: Rid of cast
                throw new ArgumentException($"Object under id {signal.IngredientId} is not an ingredient");

            _pushIngredientToInventoryCommand.Handle(inventoryId, ingredient);
            _hideCommand.Handle(signal.IngredientId);
        }
    }
}