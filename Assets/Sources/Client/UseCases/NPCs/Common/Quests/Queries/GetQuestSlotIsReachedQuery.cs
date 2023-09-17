using System;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class GetQuestSlotIsReachedQuery
    {
        private readonly IEntityRepository _repository;

        public GetQuestSlotIsReachedQuery(IEntityRepository repository) =>
            _repository = repository;

        public LiveData<bool> Handle(int questSlotId)
        {
            if (_repository.Get(questSlotId) is not QuestSlot questSlot)
                throw new InvalidCastException();

            return questSlot.IsReached;
        }
    }
}