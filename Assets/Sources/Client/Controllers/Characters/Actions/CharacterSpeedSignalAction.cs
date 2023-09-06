using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Commands;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterSpeedSignalAction : ISignalAction<CharacterSpeedSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly SetAnimationSpeedCommand _setAnimationSpeedCommand;

        public CharacterSpeedSignalAction
        (
            ICurrentPlayerService currentPlayerService,
            SetAnimationSpeedCommand setAnimationSpeedCommand
        )
        {
            _currentPlayerService = currentPlayerService;
            _setAnimationSpeedCommand = setAnimationSpeedCommand;
        }

        public void Handle(CharacterSpeedSignal signal)
        {
            _setAnimationSpeedCommand.Handle(_currentPlayerService.CharacterId, signal.Speed);
        }
    }
}