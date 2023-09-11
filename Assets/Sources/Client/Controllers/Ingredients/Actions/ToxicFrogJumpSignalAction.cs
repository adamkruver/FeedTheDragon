using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Common.Components.Destinations.Commands;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.Actions
{
    public class ToxicFrogJumpSignalAction : ISignalAction<ToxicFrogJumpSignal>
    {
        private readonly SetDestinationCommand _setDestinationCommand;
        private readonly SetSpeedCommand _setSpeedCommand;

        public ToxicFrogJumpSignalAction(SetDestinationCommand setDestinationCommand, SetSpeedCommand setSpeedCommand)
        {
            _setDestinationCommand = setDestinationCommand;
            _setSpeedCommand = setSpeedCommand;
        }
        
        public void Handle(ToxicFrogJumpSignal signal)
        {
            Debug.Log("Hopping!");
            
            _setSpeedCommand.Handle(signal.Id, signal.MovementSpeed);
            _setDestinationCommand.Handle(signal.Id, signal.Destination);
        }
    }
}