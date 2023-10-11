using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Cameras.Types;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class FishingCameraService : ILateUpdatable
    {
        private readonly ScreenRaycastService _screenRaycastService;
        private readonly FishingCamera _fishingCamera;

        public FishingCameraService
        (
            ScreenRaycastService screenRaycastService,
            FishingCamera fishingCamera
        )
        {
            _screenRaycastService = screenRaycastService;
            _fishingCamera = fishingCamera;
        }

        private Vector3 LeftScreen { get; } = Vector3.zero;
        private Vector3 RightScreen => new Vector3(Screen.width, 0, 0);

        public void LateUpdate(float deltaTime)
        {
            if (_fishingCamera.IsScreenSizeChanged == false)
                return;

            _fishingCamera.SetActualSize();

            if (_screenRaycastService.TryRaycast(LeftScreen, out RaycastHit leftRaycastHit) == false)
                return;

            if (_screenRaycastService.TryRaycast(RightScreen, out RaycastHit rightRaycastHit) == false)
                return;

            _fishingCamera.ChangeWidth(leftRaycastHit.point, rightRaycastHit.point);
        }
    }
}