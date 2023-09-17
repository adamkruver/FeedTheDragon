using System;
using System.Linq;
using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Inventories;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;

namespace Sources.Client.UseCases.NPCs.Common.Quests.Commands
{
    public class GiveQuestRequiredItemCommand
    {
        private readonly IEntityRepository _entityRepository;
        private readonly ICurrentPlayerService _currentPlayerService;

        public GiveQuestRequiredItemCommand(
            IEntityRepository entityRepository,
            ICurrentPlayerService currentPlayerService
        )
        {
            _entityRepository = entityRepository;
            _currentPlayerService = currentPlayerService;
        }

        public void Handle(int questSlotId)
        {
            if (_entityRepository.Get(questSlotId) is not QuestSlot questSlot)
                throw new InvalidCastException();

            if (questSlot.IsReached.Value)
                throw new InvalidOperationException();

            if (_entityRepository.Get(_currentPlayerService.CharacterId) is not Character character)
                throw new InvalidCastException();

            if (character.TryGetComponent(out Inventory inventory) == false)
                throw new InvalidOperationException();

            InventorySlot inventorySlot = inventory.FirstOrDefault(slot => slot.HasSameType(questSlot.RequiredType));

            if (inventorySlot is null)
                throw new NullReferenceException();
            
            questSlot.Reach();
            inventorySlot.Clear();
        }
    }
}