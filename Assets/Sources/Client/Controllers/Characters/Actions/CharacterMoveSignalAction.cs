using Sources.Client.Characters;
using Sources.Client.Controllers.Characters.SIgnals;
using Sources.Client.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterMoveSignalAction : ISignalAction<CharacterMoveSignal>
    {
        private readonly IEntityRepository _entityRepository;

        public CharacterMoveSignalAction(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public void Handle(CharacterMoveSignal signal)
        {
            Character character = (Character)_entityRepository.Get(0);
            
            character.Position.Move(signal.MoveDelta);
        }
    }
}