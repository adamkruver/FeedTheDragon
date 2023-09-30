using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Pointers.Handlers
{
    public class FishingPointerHandler : IPointerHandler
    {
        private readonly CatchFishService _catchFishService;

        public FishingPointerHandler(CatchFishService catchFishService)
        {
            _catchFishService = catchFishService;
        }
        
        public void OnStart(Vector3 position)
        {
            _catchFishService.SetPointerPosition(position);
            _catchFishService.Run();
        }

        public void OnMove(Vector3 position)
        {
            _catchFishService.SetPointerPosition(position);
        }

        public void OnFinish(Vector3 position)
        {
            _catchFishService.Stop();
        }
    }
}