using Sources.Client.Controllers.Characters.SIgnals;
using Sources.Client.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterRotateSignalAction : ISignalAction<CharacterRotateSignal>
    {
        private readonly IEntityRepository _entityRepository;

        public CharacterRotateSignalAction(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public void Handle(CharacterRotateSignal signal)
        {
            Character character = (Character)_entityRepository.Get(0);
            
            character.LookDirection.Set(signal.LookDirection);
        }
    }
}