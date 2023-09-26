using System;
using Sources.Client.Domain;
using Sources.Client.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Progresses.Queries
{
    public class GetMissionIdQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetMissionIdQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public int Handle(int id)
        {
            if (_entityRepository.Get(id) is not Composite missionHolder)
                throw new InvalidCastException();

            if (missionHolder.TryGetComponent(out Mission mission) == false)
                throw new InvalidOperationException();

            return mission.Id;
        }
    }
}