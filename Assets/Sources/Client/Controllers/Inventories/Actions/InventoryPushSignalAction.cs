using System;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Domain.Entities;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Characters.InventoryComponents.Commands;
using Sources.Client.UseCases.Characters.InventoryComponents.Queries;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using UnityEngine;

namespace Sources.Client.Controllers.Inventories.Actions
{
    public class InventoryPushSignalAction : ISignalAction<PushIngredientToInventorySignal>
    {
        private readonly IEntityRepository _entityRepository;
        private readonly CanPushInventoryQuery _canPushInventoryQuery;
        private readonly PushIngredientToInventoryCommand _pushIngredientToInventoryCommand;
        private readonly HideCommand _hideCommand;
        private readonly ICurrentPlayerService _currentPlayerService;

        public InventoryPushSignalAction
        (
            IEntityRepository entityRepository,
            CanPushInventoryQuery canPushInventoryQuery,
            PushIngredientToInventoryCommand pushIngredientToInventoryCommand,
            HideCommand hideCommand,
            ICurrentPlayerService currentPlayerService
        )
        {
            _entityRepository = entityRepository;
            _canPushInventoryQuery = canPushInventoryQuery;
            _pushIngredientToInventoryCommand = pushIngredientToInventoryCommand;
            _hideCommand = hideCommand;
            _currentPlayerService = currentPlayerService;
        }

        public void Handle(PushIngredientToInventorySignal signal)
        {
            int inventoryId = _currentPlayerService.CharacterId; //todo: Get from valid source;

            IEntity ingredientEntity = _entityRepository.Get(signal.IngredientId);

            if (ingredientEntity is not Ingredient ingredient) //todo: Rid of cast
                throw new ArgumentException($"Object under id {signal.IngredientId} is not an ingredient");

            if (_canPushInventoryQuery.Handle(inventoryId) == false)
            {
                Debug.Log("Cannot push ingredient to inventory, it filled");
                return;
            }
            
            _pushIngredientToInventoryCommand.Handle(inventoryId, ingredient);
            _hideCommand.Handle(signal.IngredientId);
        }
    }
}