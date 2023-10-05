using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using Sources.Client.Utils;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Services.Fishings
{
    public class FishingCursorServiceFactory
    {
        private readonly ScreenSphereCastService _screenSphereCastService;

        public FishingCursorServiceFactory(Camera camera)
        {
            _screenSphereCastService =
                new ScreenSphereCastService(camera, LayerConstants.InteractableMask);
        }

        public FishingCursorService Create(FishingLineCursor fishingLineCursor)
        {
            return new FishingCursorService(_screenSphereCastService, fishingLineCursor);
        }
    }
}