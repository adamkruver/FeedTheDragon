using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class FishingCursorService
    {
        private readonly FishingLineCursor _fishingLineCursor;
        private readonly ScreenSphereCastService _screenSphereCastService;
        private readonly FishingBoundsService _fishingBoundsService;

        public FishingCursorService(
            ScreenSphereCastService screenSphereCastService,
            FishingBoundsService fishingBoundsService,
            FishingLineCursor fishingLineCursor
        )
        {
            _screenSphereCastService = screenSphereCastService;
            _fishingBoundsService = fishingBoundsService;
            _fishingLineCursor = fishingLineCursor;
        }

        public void SetPosition(Vector3 position)
        {
            bool hasFish = _screenSphereCastService.TryGetComponents(position, .3f, out FishCollider[] fishColliders);
            _fishingLineCursor.SetCatchStatus(hasFish);

            _fishingLineCursor.SetPosition(position);
            _fishingLineCursor.SetScale(Vector3.one * _fishingBoundsService.Scale.y);
        }

        public void Enable() =>
            _fishingLineCursor.Enable();

        public void Disable() =>
            _fishingLineCursor.Disable();
    }
}