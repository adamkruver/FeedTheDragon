using System;
using System.Linq;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Commands
{
    public class InventoryPushItemCommand : ComponentUseCaseBase<Inventory>
    {
        private readonly IEntityRepository _entityRepository;

        public InventoryPushItemCommand(IEntityRepository entityRepository) : base(entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Handle(int inventoryId, int itemId)
        {
            if(_entityRepository.Get(inventoryId) is not Inventory inventory)
                throw new InvalidCastException();

            if (_entityRepository.Get(itemId) is not Ingredient ingredient)
                throw new InvalidCastException();
            
            if (inventory.CanPush == false)
                throw new InvalidOperationException(); // TODO: Add CantPushItemException

            InventorySlot slot = inventory.FirstOrDefault(slot => slot.Item is null) ??
                                 throw new NullReferenceException();
            
            slot.Set(ingredient);
        }
    }
}