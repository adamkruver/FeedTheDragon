using Sources.Infrastructure.SignalBus.Interfaces;
using Sources.Infrastructure.SignalBus.Signals;

namespace Sources.Infrastructure.SignalBus.Actions
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