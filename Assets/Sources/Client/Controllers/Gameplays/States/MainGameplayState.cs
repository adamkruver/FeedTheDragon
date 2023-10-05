using Sources.Client.Controllers.Characters;
using Sources.Client.Domain.Pointers;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.InfrastructureInterfaces.Pointers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Cameras.Types;
using UnityEngine;

namespace Sources.Client.Controllers.Gameplays.States
{
    public class MainGameplayState : IGameplayState
    {
        private readonly ICameraFollowService _cameraFollowService;
        private readonly CharacterMovementService _characterMovementService;
        private readonly PointerService _pointerService;
        private readonly ICameraService _cameraService;
        private readonly IPointerHandler _characterPointerHandler;

        public MainGameplayState
        (
            ICameraFollowService cameraFollowService,
            IPointerHandler characterPointerHandler,
            CharacterMovementService characterMovementService,
            PointerService pointerService,
            ICameraService cameraService
        )
        {
            _cameraFollowService = cameraFollowService;
            _characterMovementService = characterMovementService;
            _pointerService = pointerService;
            _cameraService = cameraService;
            _characterPointerHandler = characterPointerHandler;
        }

        public void Enter()
        {
            _cameraService.Enable<MainCamera>();
            _pointerService.RegisterHandler(MousePointer.LeftButton, _characterPointerHandler);
            RenderSettings.fogColor = new Color(90 / 255f, 113 / 255f, 118 / 255f);
            RenderSettings.fogDensity = 0.012f;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
        }

        public void Update(float deltaTime)
        {
            _pointerService.Update(deltaTime);
            _characterMovementService.Update(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            _cameraFollowService.LateUpdate(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public void Exit()
        {
            _pointerService.UnregisterAll();
        }
    }
}