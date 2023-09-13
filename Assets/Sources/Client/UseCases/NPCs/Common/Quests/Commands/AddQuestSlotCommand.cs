using System;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.NPCs.Common.Commands
{
    public class AddQuestSlotCommand
    {
        private readonly IEntityRepository _entityRepository;

        public AddQuestSlotCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Handle(int questId, int questSlotId)
        {
            if (_entityRepository.Get(questId) is not Quest quest)
                throw new InvalidCastException();

            if (_entityRepository.Get(questSlotId) is not QuestSlot questSlot)
                throw new InvalidCastException();

            quest.Add(questSlot);
        }
    }
}