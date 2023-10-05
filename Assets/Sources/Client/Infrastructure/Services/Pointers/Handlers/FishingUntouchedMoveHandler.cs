using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Pointers.Handlers
{
    public class FishingUntouchedMoveHandler : IPointerUntouchedMoveHandler
    {
        private readonly FishingLineService _fishingLineService;
        private readonly FishingCursorService _fishingCursorService;

        public FishingUntouchedMoveHandler(
            FishingLineService fishingLineService,
            FishingCursorService fishingCursorService
        )
        {
            _fishingLineService = fishingLineService;
            _fishingCursorService = fishingCursorService;
        }

        public void OnMove(Vector3 position)
        {
            _fishingLineService.SetPosition(position);
            _fishingCursorService.SetPosition(position);
        }
    }
}