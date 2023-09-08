using System;
using Sources.Client.Domain.Entities;
using Sources.Client.Domain.Ingredients;

namespace Sources.Client.Domain.Inventories
{
    public class InventorySlot: Composite, IEntity
    {
        public InventorySlot(int id) => 
            Id = id;

        public event Action Changed;

        public int Id { get; }
        public Ingredient Item { get; private set; }
        public IIngredientType Type => Item?.Type;

        public void Set(Ingredient ingredient)
        {
            Item = ingredient;
            Changed?.Invoke();
        }
    }
}