using Sources.Character;
using Sources.Infrastructure.Signals;
using Sources.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Infrastructure.Actions
{
    public class CharacterMoveSignalAction : ISignalAction<CharacterMoveSignal>
    {
        private readonly CharacterMovement _characterMovement;

        public CharacterMoveSignalAction(CharacterMovement characterMovement)
        {
            _characterMovement = characterMovement;
        }
        
        public void Handle(CharacterMoveSignal signal)
        {
            _characterMovement.Move(signal.MoveDelta);
        }
    }
}