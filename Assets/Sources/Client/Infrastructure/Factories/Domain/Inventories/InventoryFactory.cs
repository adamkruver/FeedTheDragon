using Sources.Client.Domain.Components;
using Sources.Client.Domain.Inventories;

namespace Sources.Client.Infrastructure.Factories.Domain.Inventories
{
    public class InventoryFactory
    {
        public Inventory Create(int id)
        {
            Inventory inventory = new Inventory(id);
            
            inventory.AddComponent(new VisibilityComponent(true));

            return inventory;
        }
    }
}