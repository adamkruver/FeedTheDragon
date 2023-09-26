using System;
using Sources.Client.Domain;
using Sources.Client.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.Progresses.Queries
{
    public class CreateMissionQuery
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly IMissionProgressFactory _missionProgressFactory;

        public CreateMissionQuery(
            IIdGenerator idGenerator,
            IEntityRepository entityRepository,
            IMissionProgressFactory missionProgressFactory
        )
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _missionProgressFactory = missionProgressFactory;
        }

        public int Handle(int ownerId, int requiredAmount)
        {
            if (_entityRepository.Get(ownerId) is not Composite owner)
                throw new InvalidCastException();
            
            int id = _idGenerator.GetId();
            
            Mission mission = _missionProgressFactory.Create(id, requiredAmount);
            _entityRepository.Add(mission);
            
            owner.AddComponent(mission);
            
            Debug.Log("Mission owner id: " + owner);
            
            return id;
        }
    }
}