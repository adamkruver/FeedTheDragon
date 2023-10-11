using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using Sources.Client.Utils;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Services.Fishings
{
    public class FishingCursorServiceFactory
    {
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly ScreenSphereCastService _screenSphereCastService;

        public FishingCursorServiceFactory(Camera camera, FishingBoundsService fishingBoundsService)
        {
            _fishingBoundsService = fishingBoundsService;
            _screenSphereCastService =
                new ScreenSphereCastService(camera, LayerConstants.InteractableMask);
        }

        public FishingCursorService Create(FishingLineCursor fishingLineCursor) =>
            new FishingCursorService(_screenSphereCastService, _fishingBoundsService, fishingLineCursor);
    }
}