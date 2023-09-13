using System;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class GetInventoryIdsQuery 
    {
        private readonly IEntityRepository _repository;

        public GetInventoryIdsQuery(IEntityRepository repository)
        {
            _repository = repository;
        }

        public LiveData<int[]> Handle(int inventoryId)
        {
            if (_repository.Get(inventoryId) is not Inventory inventory)
                throw new InvalidCastException();

            return inventory.Ids;
        }
    }
}