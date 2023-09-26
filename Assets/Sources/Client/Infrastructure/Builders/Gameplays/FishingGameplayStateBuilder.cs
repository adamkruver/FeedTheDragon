using Sources.Client.Controllers.Gameplays;
using Sources.Client.Controllers.Gameplays.States;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;

namespace Sources.Client.Infrastructure.Builders.Gameplays
{
    public class FishingGameplayStateBuilder : GameplayStateBuilderBase<FishingGameplayPayload>
    {
        private readonly ICameraService _cameraService;

        public FishingGameplayStateBuilder(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        protected override IGameplayState BuildState(IStateMachine<IGameplayPayload> stateMachine, FishingGameplayPayload payload)
        {
            return new FishingGameplayState(_cameraService);
        }
    }
}