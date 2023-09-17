using System;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class GetQuestIsCompletedQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetQuestIsCompletedQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public LiveData<bool> Handle(int questId)
        {
            if (_entityRepository.Get(questId) is not Quest quest)
                throw new InvalidCastException();

            return quest.IsCompleted;
        }
    }
}