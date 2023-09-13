using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Entities;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.NPCs.Components
{
    public class Quest : Composite, IEntity, IComponent // todo: Add QuestOwner Id
    {
        private List<QuestSlot> _slots = new List<QuestSlot>();
        private MutableLiveData<int[]> _slotsIds = new MutableLiveData<int[]>(Array.Empty<int>());

        public Quest(int id)
        { 
            Id = id;
        }

        public int Id { get; }
        public LiveData<int[]> SlotsIds => _slotsIds;
        
        public void Add(QuestSlot slot)
        {
            _slots.Add(slot);
            
            _slotsIds.Value = _slots.Select(slot => slot.Id).ToArray();
        }

        public void Remove(QuestSlot slot)
        {
            _slots.Remove(slot);
            
            _slotsIds.Value = _slots.Select(slot => slot.Id).ToArray();
        }
    }
}