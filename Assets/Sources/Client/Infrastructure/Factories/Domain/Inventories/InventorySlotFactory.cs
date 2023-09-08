using Sources.Client.Domain.Components;
using Sources.Client.Domain.Inventories;

namespace Sources.Client.Infrastructure.Factories.Domain.Inventories
{
    public class InventorySlotFactory
    {
        public InventorySlot Create(int id)
        {
            InventorySlot inventorySlot = new InventorySlot(id);

            inventorySlot.AddComponent(new VisibilityComponent(true));

            return inventorySlot;
        }
    }
}