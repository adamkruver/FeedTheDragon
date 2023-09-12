using System;
using Sources.Client.Domain.NPCs;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.NPCs.Common.Commands
{
    public class AddQuestCommand
    {
        private readonly IEntityRepository _entityRepository;

        public AddQuestCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Handle(int ownerId, int questId)
        {
            if (_entityRepository.Get(ownerId) is not Ogre ogre)
                throw new InvalidCastException();
            
            if (_entityRepository.Get(questId) is not Quest quest)
                throw new InvalidCastException();
            
            ogre.AddComponent(quest);
        }
    }
}