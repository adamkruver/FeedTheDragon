using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.Infrastructure.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Pools;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Cameras.Types;

namespace Sources.Client.Controllers.Gameplays.States
{
    public class FishingGameplayState : IGameplayState
    {
        private readonly ICameraService _cameraService;
        private readonly FishSpawnService _fishSpawnService;
        private readonly FishingPointerHandler _fishingPointerHandler;
        private readonly PointerService _pointerService;
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly FishPoolService _fishPoolService;

        public FishingGameplayState
        (ICameraService cameraService,
            FishSpawnService fishSpawnService,
            FishingPointerHandler fishingPointerHandler,
            PointerService pointerService,
            FishingBoundsService fishingBoundsService,
            FishPoolService fishPoolService
        )
        {
            _cameraService = cameraService;
            _fishSpawnService = fishSpawnService;
            _fishingPointerHandler = fishingPointerHandler;
            _pointerService = pointerService;
            _fishingBoundsService = fishingBoundsService;
            _fishPoolService = fishPoolService;
        }

        public void Enter()
        {
            _cameraService.Enable<FishingCamera>();
            _fishingBoundsService.Update(0);
            _fishSpawnService.Enable();
            _pointerService.Register(_fishingPointerHandler);
        }

        public void Exit()
        {
            _fishSpawnService.Disable();
            _pointerService.Unregister(_fishingPointerHandler);
            _fishPoolService.Dispose();
        }

        public void Update(float deltaTime)
        {
            _fishingBoundsService.Update(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public void LateUpdate(float deltaTime)
        {
        }
    }
}