using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class FishingLineService
    {
        private readonly ScreenRaycastService _screenRaycastService;
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly FishingLine _fishingLine;

        private bool _isEnabled = false;

        public FishingLineService(
            ScreenRaycastService screenRaycastService,
            FishingBoundsService fishingBoundsService,
            FishingLine fishingLine
        )
        {
            _screenRaycastService = screenRaycastService;
            _fishingBoundsService = fishingBoundsService;
            _fishingLine = fishingLine;
        }

        public void SetPosition(Vector3 position)
        {
            if (_isEnabled == false)
                return;

            if (_fishingBoundsService.ContainScreenPoint(position) == false)
                return;

            if (_screenRaycastService.TryRaycast(position, out RaycastHit hit) == false)
                return;

            _fishingLine.SetEndPoint(hit.point);
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }
    }
}