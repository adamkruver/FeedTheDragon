using System;
using System.Collections;
using Sources.Client.Domain;
using Sources.Client.Infrastructure.Builders.Presentation.Fishes;
using Sources.Client.Infrastructure.Factories.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.Fishing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Client.Infrastructure.Services.Spawn
{
    public class FishSpawnService
    {
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly FishViewBuilder _fishViewBuilder;
        private readonly CoroutineMonoRunner _coroutineMonoRunner;

        private Transform _transform;

        public FishSpawnService
        (
            FishingBoundsService fishingBoundsService,
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            FishViewBuilder fishViewBuilder
        )
        {
            _fishingBoundsService = fishingBoundsService;
            _fishViewBuilder = fishViewBuilder;
            _coroutineMonoRunner = coroutineMonoRunnerFactory.Create();
        }

        public void Enable()
        {
            _coroutineMonoRunner.Run(Spawn());
        }

        public void Disable()
        {
            _coroutineMonoRunner.Stop();
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                CreateFish();
                yield return new WaitForSeconds(Random.Range(.3f, 2f));
            }
        }

        private void CreateFish()
        {
            Vector3 direction = default;
            Vector3 startPosition = default;

            Bounds bounds = _fishingBoundsService.Bounds;

            float speed = Random.Range(1f, 10f);
            float depth = Random.Range(0, bounds.size.y);

            float height = bounds.min.y + depth / 2 + bounds.size.y / 12;

            switch (GetRandomDirection())
            {
                case Direction.Left:
                    direction = Vector3.left;
                    startPosition = new Vector3(bounds.max.x, height, bounds.center.z);
                    break;

                case Direction.Right:
                    direction = Vector3.right;
                    startPosition = new Vector3(bounds.min.x, height, bounds.center.z);
                    break;
            }

            _fishViewBuilder
                .Build(startPosition, direction, speed)
                .Run();
        }

        private Direction GetRandomDirection()
        {
            return Random.Range(0, 2) == 0
                ? Direction.Left
                : Direction.Right;
        }

        // private (Vector3 leftPosition, Vector3 rightPosition) CreatePositions(float depth)
        // {
        //     float halfScreenHeight = Screen.height / 3f;
        //     float fishDepth = halfScreenHeight * depth + Screen.height / 30f;
        //
        //     Vector3 leftScreenPosition = new Vector3(-200, fishDepth, 0);
        //     Vector3 rightScreenPosition = new Vector3(Screen.width + 200, fishDepth, 0);
        //
        //     return (RaycastFromScreenPosition(leftScreenPosition), RaycastFromScreenPosition(rightScreenPosition));
        // }
    }
}