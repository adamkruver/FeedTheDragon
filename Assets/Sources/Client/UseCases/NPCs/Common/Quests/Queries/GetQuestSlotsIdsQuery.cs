using System;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class GetQuestSlotsIdsQuery 
    {
        private readonly IEntityRepository _entityRepository;

        public GetQuestSlotsIdsQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public LiveData<int[]> Handle(int questId)
        {
            if(_entityRepository.Get(questId) is not Quest quest)
                throw new InvalidCastException();
            
            return quest.SlotsIds;
        }
    }
}