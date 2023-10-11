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
            float height = _fishingCatchCursor.Height * _fishingBoundsService.Scale.y;
            Vector2 size = new Vector2(width, height);
            
            _fishingCatchCursor.SetSize(size);
        }

        public void Enable() =>
            _fishingCatchCursor.Enable();

        public void Disable() =>
            _fishingCatchCursor.Disable();
    }
}