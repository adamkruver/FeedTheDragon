using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Entities;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.NPCs.Components
{
    public class Quest : Composite, IEntity, IComponent, IEntityType // todo: Add QuestOwner Id
    {
        private readonly List<QuestSlot> _slots = new List<QuestSlot>();
        private readonly MutableLiveData<int[]> _slotsIds = new MutableLiveData<int[]>(Array.Empty<int>());
        private readonly MutableLiveData<bool> _isCompleted = new MutableLiveData<bool>();

        public Quest(int id)
        {
            Id = id;
        }

        public IEntityType EntityType => this;
        public int Id { get; }
        public LiveData<int[]> SlotsIds => _slotsIds;
        public LiveData<bool> IsCompleted => _isCompleted;

        public void Add(QuestSlot slot)
        {
            _slots.Add(slot);
            slot.IsReached.Observe(OnSlotReached);

            _slotsIds.Value = _slots.Select(slot => slot.Id).ToArray();
        }

        public void Remove(QuestSlot slot)
        {
            if (_slots.Remove(slot))
                slot.IsReached.Unobserve(OnSlotReached);

            _slotsIds.Value = _slots.Select(slot => slot.Id).ToArray();
        }

        private void OnSlotReached(bool obj) =>
            _isCompleted.Value = _slots.All(slot => slot.IsReached.Value);
    }
}