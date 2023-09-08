using System;
using System.Linq;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class InventoryPopItemQuery : ComponentUseCaseBase<Inventory>
    {
        public InventoryPopItemQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public int Handle(int inventoryId)
        {
            Inventory inventory = GetComponent(inventoryId);

            if (inventory.Count == 0)
                throw new InvalidOperationException(); // TODO: Add CantPopItemException

            InventorySlot slot = inventory.Last(slot => slot.Item is not null) ?? throw new NullReferenceException();

            Ingredient ingredient = slot.Item;
            slot.Set(null);

            return ingredient.Id;
        }
    }
}