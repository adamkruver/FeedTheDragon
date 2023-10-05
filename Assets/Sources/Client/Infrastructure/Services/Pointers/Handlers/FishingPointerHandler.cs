using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Pointers.Handlers
{
    public class FishingPointerHandler : IPointerHandler
    {
        private readonly CatchFishService _catchFishService;
        private readonly FishingLineService _fishingLineService;
        private readonly FishingCursorService _fishingCursorService;

        public FishingPointerHandler(
            CatchFishService catchFishService,
            FishingLineService fishingLineService,
            FishingCursorService fishingCursorService
        )
        {
            _catchFishService = catchFishService;
            _fishingLineService = fishingLineService;
            _fishingCursorService = fishingCursorService;
        }

        public void OnTouchStart(Vector3 position)
        {
            _fishingCursorService.Disable();
            _catchFishService.Run();
            _catchFishService.SetPointerPosition(position);
        }

        public void OnMove(Vector3 position)
        {
            _catchFishService.SetPointerPosition(position);
        }

        public void OnTouchEnd(Vector3 position)
        {
            _fishingCursorService.Enable();
            _catchFishService.Stop();
        }
    }
}