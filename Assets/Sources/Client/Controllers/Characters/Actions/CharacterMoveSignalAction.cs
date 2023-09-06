using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterMoveSignalAction : ISignalAction<CharacterMoveSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;

        public CharacterMoveSignalAction(ICurrentPlayerService currentPlayerService)
        {
            _currentPlayerService = currentPlayerService;
        }
        
        public void Handle(CharacterMoveSignal signal)
        {
            if (_currentPlayerService.Character.TryGetComponent(out PositionComponent characterPosition) == false)
                throw new NullReferenceException();
            
            characterPosition.Move(signal.MoveDelta);
        }
    }
}