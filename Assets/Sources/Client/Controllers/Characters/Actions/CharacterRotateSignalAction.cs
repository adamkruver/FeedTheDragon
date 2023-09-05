using Sources.Client.Controllers.Characters.SIgnals;
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
            _currentPlayerService.Character.LookDirection.Set(signal.LookDirection);
        }
    }
}