using System;
using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using UnityEngine;

namespace Sources.Client.UseCases.Inventories.Commands
{
    public class DropInventoryItemCommand
    {
        private readonly IEntityRepository _repository;
        private readonly ICurrentPlayerService _currentPlayerService;

        public DropInventoryItemCommand(IEntityRepository repository, ICurrentPlayerService currentPlayerService)
        {
            _repository = repository;
            _currentPlayerService = currentPlayerService;
        }

        public void Handle(int slotId)
        {
            if (_repository.Get(_currentPlayerService.CharacterId) is not Character character)
                throw new InvalidCastException();

            if (character.TryGetComponent(out PositionComponent characterPosition) == false)
                throw new InvalidOperationException();

            if (_repository.Get(slotId) is not InventorySlot inventorySlot)
                throw new InvalidCastException();

            Ingredient ingredient = inventorySlot.Item;

            if (ingredient.TryGetComponent(out PositionComponent ingredientPosition) == false)
                throw new InvalidOperationException();

            if (ingredient.TryGetComponent(out VisibilityComponent ingredientVisibility) == false)
                throw new InvalidOperationException();

            Vector3 offset = new Vector3(1, 0, -1);
            
            ingredientPosition.Set(characterPosition.Position.Value + offset);

            if (ingredient.TryGetComponent(out DestinationComponent ingredientDestination))
                ingredientDestination.Set(characterPosition.Position.Value + offset);

            ingredientVisibility.Show();
            inventorySlot.Clear();
        }
    }
}