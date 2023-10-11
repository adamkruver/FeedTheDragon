using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using Sources.Client.Utils;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Services.Fishings
{
    public class FishingLineServiceFactory
    {
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly ScreenRaycastService _screenRaycastService;

        public FishingLineServiceFactory(FishingBoundsService fishingBoundsService, Camera camera)
        {
            _fishingBoundsService = fishingBoundsService;
            _screenRaycastService = new ScreenRaycastService(camera, LayerConstants.TransparentFXMask);
        }

        public FishingLineService Create(FishingLine fishingLine) => 
            new FishingLineService(_screenRaycastService, _fishingBoundsService, fishingLine);
    }
}