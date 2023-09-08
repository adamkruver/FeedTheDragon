using System;
using Sources.Client.Domain.Inventories;
using Sources.Client.Infrastructure.Factories.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;

namespace Sources.Client.UseCases.Inventories.Slots.Queries
{
    public class CreateInventorySlotQuery
    {
        private readonly IEntityRepository _repository;
        private readonly IIdGenerator _idGenerator;
        private readonly InventorySlotFactory _inventorySlotFactory = new InventorySlotFactory();

        public CreateInventorySlotQuery(
            IEntityRepository repository,
            IIdGenerator idGenerator
        )
        {
            _repository = repository;
            _idGenerator = idGenerator;
        }

        public int Handle(int inventoryId)
        {
            if (_repository.Get(inventoryId) is not Inventory inventory)
                throw new InvalidCastException();

            int id = _idGenerator.GetId();
            InventorySlot slot = _inventorySlotFactory.Create(id);
            
            inventory.Add(slot);
            _repository.Add(slot);

            return id;
        }
    }
}