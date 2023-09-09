using System;
using System.Linq;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class InventoryPopItemQuery
    {
        private readonly IEntityRepository _repository;

        public InventoryPopItemQuery(IEntityRepository repository)
        {
            _repository = repository;
        }

        public int Handle(int inventoryId)
        {
            if (_repository.Get(inventoryId) is not Inventory inventory)
                throw new InvalidCastException();
            
            if (inventory.Count == 0)
                throw new InvalidOperationException(); // TODO: Add CantPopItemException

            InventorySlot slot = inventory.Last(slot => slot.Item is not null) ?? throw new NullReferenceException();

            Ingredient ingredient = slot.Item;
            slot.Set(null);

            return ingredient.Id;
        }
    }
}