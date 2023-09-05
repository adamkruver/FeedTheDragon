using Sources.Client.Controllers.Characters.SIgnals;
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
            _currentPlayerService.Character.Position.Move(signal.MoveDelta);
        }
    }
}