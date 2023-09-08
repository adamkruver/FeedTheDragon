using System;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Inventories.Slots.Commands
{
    public class SetSlotItemCommand
    {
        private readonly IEntityRepository _repository;

        public SetSlotItemCommand(IEntityRepository repository) => 
            _repository = repository;

        public void Handle(int slotId, int itemId)
        {
            if (_repository.Get(slotId) is not InventorySlot slot)
                throw new InvalidCastException();

            if (_repository.Get(slotId) is not Ingredient ingredient)
                throw new InvalidCastException();

            slot.Set(ingredient);
        }
    }
}