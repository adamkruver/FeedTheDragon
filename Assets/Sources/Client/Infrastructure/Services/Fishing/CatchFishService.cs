using System.Collections.Generic;
using System.Linq;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Factories.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.Raycasts;
using Sources.Client.Presentation.Views.Fishing;
using Sources.Client.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Client.Infrastructure.Services.Fishing
{
    public class CatchFishService : IUpdatable
    {
        private readonly CoroutineMonoRunner _coroutineMonoRunner;
        private readonly ScreenSphereCastService _screenSphereCastService;
        private readonly ScreenRayCastService _screenRayCastService;

        private RectTransform _catchCursorRectTransform;
        private RectTransform _cursor;
        private RectTransform _water;
        private Image _catchCursor;
        private FishingLine _fishingLine;

        private Vector3 _pointerPosition;
        private Vector3 _fishPosition;
        private FishView _fish;

        private bool _isRunning;

        public CatchFishService
        (
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            Camera camera
        )
        {
            int transparentMask = 1 << LayerMask.NameToLayer(LayerConstants.TransparentFX);
            int interactableMask = 1 << LayerMask.NameToLayer(LayerConstants.Interactable);

            _screenRayCastService = new ScreenRayCastService(camera, transparentMask);
            _screenSphereCastService = new ScreenSphereCastService(camera, interactableMask);

            _coroutineMonoRunner = coroutineMonoRunnerFactory.Create();
        }

        public void Run()
        {
            if (_screenSphereCastService.TryGetComponents(Input.mousePosition, 1f, out FishCollider[] fishColliders) == false)
                return;

            _isRunning = true;

            FishView fishView = GetFish(fishColliders);

            _catchCursorRectTransform.gameObject.SetActive(true);
            _cursor.gameObject.SetActive(false);
            //_coroutineMonoRunner.Run();
        }

        public void SetPointerPosition(Vector3 position)
        {
        }

        public void Stop()
        {
            _catchCursorRectTransform.gameObject.SetActive(false);
            _cursor.gameObject.SetActive(true);

            _isRunning = false;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            float radius = 5f;

            while (_fishPosition.y < _water.sizeDelta.y &&
                   _screenSphereCastService.CheckCollision(_pointerPosition, radius, _fish))
            {
                Vector3 nextPosition = _pointerPosition + Vector3.up;
                _pointerPosition = Vector3.MoveTowards(_pointerPosition, nextPosition, 500 * Time.deltaTime);

                if (_screenRayCastService.TryRaycast(_pointerPosition, out RaycastHit hit))
                {
                    _fishingLine.SetEndPoint(hit.point);
                }

                radius = Mathf.Max(radius - 0.05f, 2);

                _catchCursorRectTransform.anchoredPosition = _pointerPosition;
                _catchCursor.transform.localScale = Vector3.one * radius;
            }
        }

        private FishView GetFish(IEnumerable<FishCollider> fishColliders)
        {
            return fishColliders.First()?.FishView;
        }
    }
}