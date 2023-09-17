using System;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.Infrastructure.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class CreateQuestSlotQuery
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IEntityRepository _entityRepository;
        private readonly QuestSlotFactory _questSlotFactory;

        public CreateQuestSlotQuery(IIdGenerator idGenerator, IEntityRepository entityRepository,
            QuestSlotFactory questSlotFactory)
        {
            _idGenerator = idGenerator;
            _entityRepository = entityRepository;
            _questSlotFactory = questSlotFactory;
        }

        public int Handle(IIngredientType requiredType, int questId)
        {
            if (_entityRepository.Get(questId) is not Quest quest)
                throw new InvalidCastException();

            int id = _idGenerator.GetId();

            QuestSlot questSlot = _questSlotFactory.Create(id, requiredType);
            _entityRepository.Add(questSlot);
            
            quest.Add(questSlot);

            return id;
        }
    }
}