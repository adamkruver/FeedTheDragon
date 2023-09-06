using System;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.Positions.Commands;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CharacterMoveSignalAction : ISignalAction<CharacterMoveSignal>
    {
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly MovePositionCommand _movePositionCommand;

        public CharacterMoveSignalAction
        (
            ICurrentPlayerService currentPlayerService,
            MovePositionCommand movePositionCommand
        )
        {
            _currentPlayerService = currentPlayerService;
            _movePositionCommand = movePositionCommand;
        }

        public void Handle(CharacterMoveSignal signal)
        {
            _movePositionCommand.Handle(_currentPlayerService.CharacterId, signal.MoveDelta);
        }
    }
}