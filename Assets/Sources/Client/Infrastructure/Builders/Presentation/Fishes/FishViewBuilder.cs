using Sources.Client.Infrastructure.Services.Pools;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Builders.Presentation.Fishes
{
    public class FishViewBuilder
    {
        private FishPoolService _fishPoolService;

        public FishViewBuilder(FishPoolService fishPoolService)
        {
            _fishPoolService = fishPoolService;
        }

        public FishView Build(Vector3 startPosition, Vector3 direction, float speed)
        {
            FishView fishView = _fishPoolService.Get();

            fishView.Init(startPosition, direction, speed);

            return fishView;
        }
    }
}