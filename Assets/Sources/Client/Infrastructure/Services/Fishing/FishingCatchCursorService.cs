using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class FishingCatchCursorService
    {
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly FishingCatchCursor _fishingCatchCursor;

        public FishingCatchCursorService(FishingBoundsService fishingBoundsService,
            FishingCatchCursor fishingCatchCursor)
        {
            _fishingBoundsService = fishingBoundsService;
            _fishingCatchCursor = fishingCatchCursor;
        }

        public void SetPosition(Vector3 position)
        {
            _fishingCatchCursor.SetPosition(position);
        }

        public void SetWidth(float width)
        {
            Vector2 size = new Vector2(width, _fishingBoundsService.BoundsSize.y - _fishingCatchCursor.Position.y);

            _fishingCatchCursor.SetSize(size);
        }

        public void Enable() =>
            _fishingCatchCursor.Enable();

        public void Disable() =>
            _fishingCatchCursor.Disable();
    }
}