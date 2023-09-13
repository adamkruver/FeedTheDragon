using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Entities;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.Inventories
{
    public class Inventory : Composite, IEntity, IComponent, IEnumerable<InventorySlot>
    {
        private readonly List<InventorySlot> _slots = new List<InventorySlot>();
        private readonly MutableLiveData<int[]> _ids = new MutableLiveData<int[]>(new int[] { });

        public Inventory(int id) =>
            Id = id;

        public int Id { get; }
        public int Capacity => _slots.Count;
        public int Count => _slots.Count(slot => slot.Item is not null);
        public bool CanPush => Count < Capacity;
        public LiveData<int[]> Ids => _ids;

        public InventorySlot this[int i] => _slots[i];

        public void Add(InventorySlot slot)
        {
            _slots.Add(slot);
            _ids.Value = _slots.Select(slot => slot.Id).ToArray();
        }

        public void Remove(InventorySlot slot)
        {
            _slots.Remove(slot);
            _ids.Value = _slots.Select(slot => slot.Id).ToArray();
        }

        public IEnumerator<InventorySlot> GetEnumerator() =>
            _slots.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}