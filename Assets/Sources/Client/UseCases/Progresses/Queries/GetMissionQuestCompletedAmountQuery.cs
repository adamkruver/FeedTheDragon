using System;
using Sources.Client.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.Progresses.Queries
{
    public class GetMissionQuestCompletedAmountQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetMissionQuestCompletedAmountQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public LiveData<int> Handle(int id)
        {
            if (_entityRepository.Get(id) is not Mission mission) 
                throw new NullReferenceException();
            
            return mission.CompletedAmount;
        }
    }
}