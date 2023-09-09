using System;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveData;

namespace Sources.Client.UseCases.Inventories.Slots.Queries
{
    public class GetInventorySlotItemTypeQuery
    {
        private readonly IEntityRepository _repository;

        public GetInventorySlotItemTypeQuery(IEntityRepository repository)
        {
            _repository = repository;
        }

        public LiveData<Type> Handle(int id)
        {
            if (_repository.Get(id) is not InventorySlot slot)
                throw new InvalidCastException();

            return slot.Type;
        }
    }
}