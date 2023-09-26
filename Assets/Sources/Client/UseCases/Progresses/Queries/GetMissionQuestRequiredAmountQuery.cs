using System;
using Sources.Client.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.UseCases.Progresses.Queries
{
    public class GetMissionQuestRequiredAmountQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetMissionQuestRequiredAmountQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public int Handle(int id)
        {
            if (_entityRepository.Get(id) is not Mission mission) 
                throw new NullReferenceException();
            
            return mission.RequiredAmount;
        }
    }
}