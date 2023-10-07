using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Factories.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.Presentation.Cameras.Types;
using Sources.Client.Presentation.Views.Fishing;
using Sources.Client.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class CatchFishService : IUpdatable
    {
        private readonly FishingBoundsService _fishingBoundsService;
        private readonly FishingLineService _fishingLineService;
        private readonly FishingCatchCursorService _fishingCatchCursorService;
        private readonly CoroutineMonoRunner _coroutineMonoRunner;
        private readonly ScreenSphereCastService _screenSphereCastService;
        private readonly ScreenRayCastService _screenRayCastService;
        private readonly Camera _camera;

        private FishView _caughtFish;

        private Vector3 _pointerPosition;

        private bool _isRunning;

        public CatchFishService
        (
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            Camera camera,
            FishingBoundsService fishingBoundsService,
            FishingLineService fishingLineService,
            ICameraProvider cameraProvider,
            FishingCatchCursorService fishingCatchCursorService
        )
        {
            _fishingBoundsService = fishingBoundsService;
            _fishingLineService = fishingLineService;
            _fishingCatchCursorService = fishingCatchCursorService;
            _camera = cameraProvider.Get<FishingCamera>();

            int transparentMask = 1 << LayerMask.NameToLayer(LayerConstants.TransparentFX);
            int interactableMask = 1 << LayerMask.NameToLayer(LayerConstants.Interactable);

            _screenRayCastService = new ScreenRayCastService(camera, transparentMask);
            _screenSphereCastService = new ScreenSphereCastService(camera, interactableMask);

            _coroutineMonoRunner = coroutineMonoRunnerFactory.Create();
        }

        private float _minRatio = 5;
        private float _maxRatio = 2;

        private float _tanAlfa => (_minRatio - _maxRatio) / (2 * (_minRatio * _maxRatio)) * _fishingBoundsService.Ratio;

        public void Run()
        {
            if (_screenSphereCastService.TryGetComponents(Input.mousePosition, 1f, out FishCollider[] fishColliders) ==
                false)
                return;

            _isRunning = true;

            _caughtFish = GetFish(fishColliders);
            _caughtFish.Catch();

            _coroutineMonoRunner.Run(ChangeFishDirection());
            _fishingCatchCursorService.Enable();
        }

        public void SetPointerPosition(Vector3 position)
        {
            _pointerPosition = position;
        }

        public void Stop()
        {
            _fishingCatchCursorService.Disable();
            _coroutineMonoRunner.Stop();
            _isRunning = false;

            if (_caughtFish != null)
                _caughtFish.Swim();
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            float minWidth = _fishingBoundsService.BoundsSize.x / _minRatio;

            Vector3 fishCameraPosition = _camera.WorldToScreenPoint(_caughtFish.transform.position);
            _fishingLineService.SetPosition(fishCameraPosition);

            Vector3 catchCursorPosition = new Vector3(_pointerPosition.x, fishCameraPosition.y);

            _fishingCatchCursorService.SetPosition(catchCursorPosition);

            float minSide = catchCursorPosition.x - (minWidth / 2f);
            float maxSide = catchCursorPosition.x + (minWidth / 2f);

            float deltaY = _fishingBoundsService.BoundsSize.y - catchCursorPosition.y;

            if (fishCameraPosition.x > minSide && fishCameraPosition.x < maxSide)
            {
                float otrezok = _tanAlfa * deltaY;
                _fishingCatchCursorService.SetWidth(minWidth + (otrezok * 2));
                
                if (_caughtFish.transform.position.y >= _fishingBoundsService.Bounds.max.y)
                {
                    Debug.Log("Рыбка выловлена");
                    Stop();
                    _caughtFish.Disable();
                }
            }
            else
            {
                Stop();
            }
        }


        private FishView GetFish(IEnumerable<FishCollider> fishColliders)
        {
            return fishColliders.First()?.FishView;
        }

        private IEnumerator ChangeFishDirection()
        {
            while (true)
            {
                float segment = _fishingBoundsService.BoundsSize.x / 8;
                float x = Random.Range(0, 1f) * segment + segment;
                x *= Random.Range(0, 1f) < 0.5f ? -1 : 1;

                Vector3 fishCameraPosition = _camera.WorldToScreenPoint(_caughtFish.transform.position);

                float fishX = fishCameraPosition.x + x;

                if (fishX < 0 || fishX > _fishingBoundsService.BoundsSize.x)
                    x *= -1;

                Vector2 fishTargetPosition = new Vector2(x, _fishingBoundsService.BoundsSize.y);
                fishTargetPosition.Normalize();

                Vector3 direction = new Vector3(fishTargetPosition.x, 0.04f, 0);
                _caughtFish.SetDirection(direction);
                _caughtFish.SetViewDirection(direction);

                _caughtFish.SetSpeed(9f);

                float time = Random.Range(0.5f, 1f);
                yield return new WaitForSeconds(time);
            }
        }
    }
}