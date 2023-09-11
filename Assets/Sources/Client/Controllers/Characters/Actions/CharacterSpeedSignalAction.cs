using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterSpeedSignalAction : ISignalAction<CharacterSpeedSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly SetSpeedCommand _setSpeedCommand;

        public CharacterSpeedSignalAction
        (
            ICurrentPlayerService currentPlayerService,
            SetSpeedCommand setSpeedCommand
        )
        {
            _currentPlayerService = currentPlayerService;
            _setSpeedCommand = setSpeedCommand;
        }

        public void Handle(CharacterSpeedSignal signal)
        {
            _setSpeedCommand.Handle(_currentPlayerService.CharacterId, signal.Speed);
        }
    }
}