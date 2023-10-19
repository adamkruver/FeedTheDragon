using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Entities;
using Sources.Client.Domain.NPCs.Components;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.Progresses
{
    public class Mission : Composite, IEntity, IComponent, IEntityType
    {
        private readonly List<Quest> _quests = new List<Quest>();
        private readonly MutableLiveData<int> _completedAmount = new MutableLiveData<int>();
        private readonly MutableLiveData<bool> _isCompleted = new MutableLiveData<bool>();
        private readonly MutableLiveData<int[]> _questIds = new MutableLiveData<int[]>(Array.Empty<int>());

        public Mission(int id, int requiredAmount)
        {
            Id = id;
            RequiredAmount = requiredAmount;
        }

        public IEntityType EntityType => this;
        public int Id { get; }
        public int RequiredAmount { get; }
        public LiveData<int> CompletedAmount => _completedAmount;

        public LiveData<bool> IsCompleted => _isCompleted;
        public LiveData<int[]> QuestIds => _questIds;

        public void Add(Quest quest)
        {
            _quests.Add(quest);
            quest.IsCompleted.Observe(OnQuestCompleted);

            _questIds.Value = _quests.Select(quest => quest.Id).ToArray();
        }

        public void Remove(Quest quest)
        {
            if (_quests.Remove(quest))
                quest.IsCompleted.Unobserve(OnQuestCompleted);

            _questIds.Value = _quests.Select(quest => quest.Id).ToArray();
        }

        private void OnQuestCompleted(bool obj)
        {
            _completedAmount.Value = _quests.FindAll(quest => quest.IsCompleted.Value).Count;
            _isCompleted.Value = _quests.All(quest => quest.IsCompleted.Value);
        }
    }
}