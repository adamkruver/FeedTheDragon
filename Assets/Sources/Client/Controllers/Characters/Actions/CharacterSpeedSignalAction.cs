using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterSpeedSignalAction : ISignalAction<CharacterSpeedSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;

        public CharacterSpeedSignalAction(ICurrentPlayerService currentPlayerService)
        {
            _currentPlayerService = currentPlayerService;
        }

        public void Handle(CharacterSpeedSignal signal)
        {
            if (_currentPlayerService.Character.TryGetComponent(out SpeedComponent characterSpeed) == false)
                throw new NullReferenceException();
            
            characterSpeed.Set(signal.Speed);
        }
    }
}