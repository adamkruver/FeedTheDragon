using System;
using Sources.Client.Domain;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class CreateQuestQuery
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IQuestFactory _questFactory;
        private readonly IEntityRepository _entityRepository;

        public CreateQuestQuery
        (
            IIdGenerator idGenerator,
            IQuestFactory questFactory,
            IEntityRepository entityRepository
        )
        {
            _idGenerator = idGenerator;
            _questFactory = questFactory;
            _entityRepository = entityRepository;
        }

        public int Handle(int ownerId)
        {
            if (_entityRepository.Get(ownerId) is not Composite owner)
                throw new InvalidCastException();

            int id = _idGenerator.GetId();

            Quest quest = _questFactory.Create(id);
            _entityRepository.Add(quest);

            owner.AddComponent(quest);

            return id;
        }
    }
}