using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Inventories
{
    public class Inventory : Composite, IEntity, IComponent, IEnumerable<InventorySlot>
    {
        private readonly List<InventorySlot> _slots = new List<InventorySlot>();
        
        public Inventory(int id) => 
            Id = id;

        public event Action Changed;

        public int Id { get; }

        public int Capacity => _slots.Count;
        public int Count => _slots.Count(slot => slot.Item is not null);
        public bool CanPush => Count < Capacity;
        
        public InventorySlot this[int i] => _slots[i];

        public void Add(InventorySlot slot)
        {
            slot.Changed += InvokeChanges;
            _slots.Add(slot);
            Changed?.Invoke();
        }

        public void Remove(InventorySlot slot)
        {
            slot.Changed -= InvokeChanges;
            _slots.Remove(slot);
            Changed?.Invoke();
        }

        public IEnumerator<InventorySlot> GetEnumerator() => 
            _slots.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();

        private void InvokeChanges() =>
            Changed?.Invoke();
    }
}