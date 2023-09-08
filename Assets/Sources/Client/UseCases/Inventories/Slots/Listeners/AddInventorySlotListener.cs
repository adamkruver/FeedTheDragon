using System;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Inventories.Slots.Listeners
{
    public class AddInventorySlotListener
    {
        private readonly IEntityRepository _repository;

        public AddInventorySlotListener(IEntityRepository repository)
        {
            _repository = repository;
        }

        public void Handle(int id, Action callback)
        {
            if (_repository.Get(id) is not InventorySlot slot)
                throw new InvalidCastException();

            slot.Changed += callback;
        }
    }
}