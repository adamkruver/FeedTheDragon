using System;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Inventories.Slots.Commands
{
    public class RemoveSlotItemCommand
    {
        private readonly IEntityRepository _repository;

        public RemoveSlotItemCommand(IEntityRepository repository) => 
            _repository = repository;

        public void Handle(int slotId)
        {
            if (_repository.Get(slotId) is not InventorySlot slot)
                throw new InvalidCastException();

            slot.Set(null);
        }
    }
}