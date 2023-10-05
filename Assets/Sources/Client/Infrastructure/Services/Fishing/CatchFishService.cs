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
        private readonly CoroutineMonoRunner _coroutineMonoRunner;
        private readonly ScreenSphereCastService _screenSphereCastService;
        private readonly ScreenRayCastService _screenRayCastService;
        private readonly Camera _camera;

        // private RectTransform _catchCursorRectTransform;
        // private RectTransform _cursor;
        // private RectTransform _water;
        // private Image _catchCursor;
        // private FishingLine _fishingLine;
        //
        // private Vector3 _pointerPosition;
        // private Vector3 _fishPosition;
        private FishView _caughtFish;

        private Vector3 _pointerPosition;

        private bool _isRunning;

        public CatchFishService
        (
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            Camera camera,
            FishingBoundsService fishingBoundsService,
            FishingLineService fishingLineService,
            ICameraProvider cameraProvider
        )
        {
            _fishingBoundsService = fishingBoundsService;
            _fishingLineService = fishingLineService;
            _camera = cameraProvider.Get<FishingCamera>();
            
            int transparentMask = 1 << LayerMask.NameToLayer(LayerConstants.TransparentFX);
            int interactableMask = 1 << LayerMask.NameToLayer(LayerConstants.Interactable);

            _screenRayCastService = new ScreenRayCastService(camera, transparentMask);
            _screenSphereCastService = new ScreenSphereCastService(camera, interactableMask);

            _coroutineMonoRunner = coroutineMonoRunnerFactory.Create();
        }

        public void Run()
        {
            if (_screenSphereCastService.TryGetComponents(Input.mousePosition, 1f, out FishCollider[] fishColliders) ==
                false)
                return;

            _isRunning = true;

            _caughtFish = GetFish(fishColliders);
            _caughtFish.Catch();

            _coroutineMonoRunner.Run(ChangeFishDirection());
        }

        public void SetPointerPosition(Vector3 position)
        {
            _pointerPosition = position;
        }

        public void Stop()
        {
            _coroutineMonoRunner.Stop();
            _isRunning = false;

            if (_caughtFish != null)
                _caughtFish.Swim();
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;
            
            Vector3 fishCameraPosition = _camera.WorldToScreenPoint(_caughtFish.transform.position);
            _fishingLineService.SetPosition(fishCameraPosition);

            //float radius = 5f;

            // while (_pointerPosition.y < _fishingBoundsService.BoundsSize.y &&
            //        _screenSphereCastService.CheckCollision(_pointerPosition, radius, _caughtFish))
            // {
            //     Vector3 nextPosition = _pointerPosition + Vector3.up;
            //     _pointerPosition = Vector3.MoveTowards(_pointerPosition, nextPosition, 500 * deltaTime);
            //
            //     if (_screenRayCastService.TryRaycast(_pointerPosition, out RaycastHit hit))
            //     {
            //         _fishingLine.SetEndPoint(hit.point);
            //     }
            //
            //     radius = Mathf.Max(radius - 0.05f, 2);
            //
            //     _catchCursorRectTransform.anchoredPosition = _pointerPosition;
            //     _catchCursor.transform.localScale = Vector3.one * radius;
            // }
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

                _caughtFish.SetSpeed(15f);

                float time = Random.Range(0.5f, 1f);
                yield return new WaitForSeconds(time);
            }
        }
    }
}