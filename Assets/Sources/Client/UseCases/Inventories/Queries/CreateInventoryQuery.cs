using System;
using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Inventories;
using Sources.Client.Infrastructure.Factories.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class CreateInventoryQuery
    {
        private readonly IEntityRepository _repository;
        private readonly IIdGenerator _idGenerator;
        private readonly InventoryFactory _inventoryFactory = new InventoryFactory();

        public CreateInventoryQuery(
            IEntityRepository repository,
            IIdGenerator idGenerator
        )
        {
            _repository = repository;
            _idGenerator = idGenerator;
        }

        public int Handle(int ownerId)
        {
            if (_repository.Get(ownerId) is not Character character)
                throw new InvalidCastException();

            int id = _idGenerator.GetId();
            Inventory inventory = _inventoryFactory.Create(id);

            character.AddComponent(inventory);
            _repository.Add(inventory);

            return id;
        }
    }
}