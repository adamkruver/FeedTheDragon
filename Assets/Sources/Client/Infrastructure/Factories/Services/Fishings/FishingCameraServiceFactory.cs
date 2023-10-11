using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Cameras.Types;
using Sources.Client.Utils;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Services.Fishings
{
    public class FishingCameraServiceFactory
    {
        private readonly FishingCamera _fishingCamera;
        private readonly ScreenRaycastService _screenRaycastService;

        public FishingCameraServiceFactory(Camera camera, FishingCamera fishingCamera)
        {
            _fishingCamera = fishingCamera;
            _screenRaycastService = new ScreenRaycastService(camera, LayerConstants.TransparentFXMask);
        }

        public FishingCameraService Create() =>
            new FishingCameraService(_screenRaycastService, _fishingCamera);
    }
}