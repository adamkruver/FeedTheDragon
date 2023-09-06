using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterRotateSignalAction : ISignalAction<CharacterRotateSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;

        public CharacterRotateSignalAction(ICurrentPlayerService currentPlayerService)
        {
            _currentPlayerService = currentPlayerService;
        }
        
        public void Handle(CharacterRotateSignal signal)
        {
            if (_currentPlayerService.Character.TryGetComponent(out LookDirectionComponent characterLookDirection) == false)
                throw new NullReferenceException();
            
            characterLookDirection.Set(signal.LookDirection);
        }
    }
}