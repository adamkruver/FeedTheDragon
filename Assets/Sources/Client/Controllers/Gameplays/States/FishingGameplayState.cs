using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Cameras.Types;

namespace Sources.Client.Controllers.Gameplays.States
{
    public class FishingGameplayState : IGameplayState
    {
        private readonly ICameraService _cameraService;

        public FishingGameplayState(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void Enter()
        {
            _cameraService.Enable<FishingCamera>();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public void LateUpdate(float deltaTime)
        {
        }
    }
}