using Sources.Client.Controllers.Characters.SIgnals;
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
            _currentPlayerService.Character.Speed.Set(signal.Speed);
        }
    }
}