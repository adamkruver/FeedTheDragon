using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class FishingCursorService
    {
        private readonly ScreenRayCastService _screenRayCastService;
        private readonly FishingLineCursor _fishingLineCursor;
        private readonly ScreenSphereCastService _screenSphereCastService;

        public FishingCursorService(
            ScreenSphereCastService screenSphereCastService,
            FishingLineCursor fishingLineCursor
        )
        {
            _screenSphereCastService = screenSphereCastService;
            _fishingLineCursor = fishingLineCursor;
        }

        public void SetPosition(Vector3 position)
        {
            bool hasFish = _screenSphereCastService.TryGetComponents(position, .3f, out FishCollider[] fishColliders);
            _fishingLineCursor.SetCatchStatus(hasFish);

            _fishingLineCursor.SetPosition(position);
        }

        public void Enable() =>
            _fishingLineCursor.Enable();

        public void Disable() =>
            _fishingLineCursor.Disable();
    }
}