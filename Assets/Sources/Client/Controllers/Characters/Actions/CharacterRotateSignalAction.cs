using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.LookDirections.Commands;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterRotateSignalAction : ISignalAction<CharacterRotateSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly SetLookDirectionCommand _lookDirectionCommand;

        public CharacterRotateSignalAction
        (
            ICurrentPlayerService currentPlayerService,
            SetLookDirectionCommand lookDirectionCommand
        )
        {
            _currentPlayerService = currentPlayerService;
            _lookDirectionCommand = lookDirectionCommand;
        }

        public void Handle(CharacterRotateSignal signal)
        {
            _lookDirectionCommand.Handle(_currentPlayerService.CharacterId, signal.LookDirection);
        }
    }
}