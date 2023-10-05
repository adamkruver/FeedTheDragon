using Sources.Client.Domain.Pointers;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.Infrastructure.Services.Pools;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.InfrastructureInterfaces.Pointers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Cameras.Types;
using UnityEngine;

namespace Sources.Client.Controllers.Gameplays.States
{
    public class FishingGameplayState : IGameplayState
    {
        private readonly ICameraService _cameraService;
        private readonly FishSpawnService _fishSpawnService;
        private readonly IPointerUntouchedMoveHandler _pointerUntouchedMoveHandler;
        private readonly IPointerHandler _pointerHandler;
        private readonly PointerService _pointerService;
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly FishPoolService _fishPoolService;
        private readonly FishingLineService _fishingLineService;
        private readonly FishingCursorService _fishingCursorService;

        public FishingGameplayState
        (ICameraService cameraService,
            FishSpawnService fishSpawnService,
            IPointerHandler pointerHandler,
            IPointerUntouchedMoveHandler pointerUntouchedMoveHandler,
            PointerService pointerService,
            FishingBoundsService fishingBoundsService,
            FishPoolService fishPoolService,
            FishingLineService fishingLineService,
            FishingCursorService fishingCursorService
        )
        {
            _cameraService = cameraService;
            _fishSpawnService = fishSpawnService;
            _pointerUntouchedMoveHandler = pointerUntouchedMoveHandler;
            _pointerHandler = pointerHandler;
            _pointerService = pointerService;
            _fishingBoundsService = fishingBoundsService;
            _fishPoolService = fishPoolService;
            _fishingLineService = fishingLineService;
            _fishingCursorService = fishingCursorService;
        }

        public void Enter()
        {
            _cameraService.Enable<FishingCamera>();
            _fishingBoundsService.Update(0);
            _fishSpawnService.Enable();
            _pointerService.RegisterHandler(MousePointer.LeftButton, _pointerHandler);
            _pointerService.RegisterUntouchedMoveHandler(_pointerUntouchedMoveHandler);
            _fishingCursorService.Enable();
            _fishingLineService.Enable();
            RenderSettings.fogColor = new Color(41 / 255f, 201 / 255f, 204 / 255f);
            RenderSettings.fogDensity = 0.032f;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 16;
            RenderSettings.fogEndDistance = 52;
        }

        public void Exit()
        {
            _fishSpawnService.Disable();
            _fishingCursorService.Disable();
            _fishingLineService.Disable();
            _pointerService.UnregisterAll();
            _fishPoolService.Dispose();
        }

        public void Update(float deltaTime)
        {
            _fishingBoundsService.Update(deltaTime);
            _pointerService.Update(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public void LateUpdate(float deltaTime)
        {
        }
    }
}