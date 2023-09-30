using System;
using System.Collections.Generic;
using System.Linq;
using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Infrastructure.Factories.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Presentation.PoolComponents;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine.Pool;
using Environment = Sources.Client.App.Configs.Environment;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Sources.Client.Infrastructure.Services.Pools
{
    public class FishPoolService : IDisposable
    {
        private readonly string[] _prefabPaths;
        private readonly IPrefabFactory _prefabFactory;
        private readonly CoroutineMonoRunnerFactory _coroutineMonoRunnerFactory;
        private readonly FishingBoundsService _fishingBoundsService;

        private IObjectPool<FishView> _objectPool;
        private bool _isDisposed;

        public FishPoolService
        (
            IPrefabFactory prefabFactory,
            Environment environment,
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            FishingBoundsService fishingBoundsService
        )
        {
            _prefabPaths = environment.Fishes.Values.ToArray();
            _prefabFactory = prefabFactory;
            _coroutineMonoRunnerFactory = coroutineMonoRunnerFactory;
            _fishingBoundsService = fishingBoundsService;

            _objectPool = new ObjectPool<FishView>
            (
                OnCreate,
                OnTakeFromPool,
                OnReturnToPool,
                OnDestroyPoolObject
            );
        }

        public FishView Get() =>
            _objectPool.Get();

        public void Dispose()
        {
            _isDisposed = true;
            _objectPool.Clear();
        }

        private FishView OnCreate()
        {
            FishView fishView = _prefabFactory.Create<FishView>(_prefabPaths[Random.Range(0, _prefabPaths.Length)]);

            FishPoolComponent fishPoolComponent = fishView.gameObject.AddComponent<FishPoolComponent>();
            fishPoolComponent.SetPool(_objectPool, fishView);

            CoroutineMonoRunner coroutineMonoRunner = _coroutineMonoRunnerFactory.Create();

            fishView.Construct(coroutineMonoRunner, _fishingBoundsService);

            return fishView;
        }

        private void OnTakeFromPool(FishView fish)
        {
            fish.gameObject.SetActive(true);
        }

        private void OnReturnToPool(FishView fish)
        {
            fish.gameObject.SetActive(false);

            if (_isDisposed)
                OnDestroyPoolObject(fish);
        }

        private void OnDestroyPoolObject(FishView fish)
        {
            Object.Destroy(fish.gameObject);
        }
    }
}