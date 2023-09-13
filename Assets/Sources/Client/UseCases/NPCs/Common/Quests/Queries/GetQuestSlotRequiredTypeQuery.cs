using System;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Queries
{
    public class GetQuestSlotRequiredTypeQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetQuestSlotRequiredTypeQuery(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IIngredientType Handle(int questSlotId)
        {
            if (_entityRepository.Get(questSlotId) is not QuestSlot questSlot)
                throw new InvalidCastException();
            
            return questSlot.RequiredType;
        }
    }
}