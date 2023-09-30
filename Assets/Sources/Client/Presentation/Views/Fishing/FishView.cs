using System;
using System.Collections;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Presentation.PoolComponents;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishView : MonoBehaviour
    {
        [SerializeField] private float _speedMultiplier = 1f;
        [SerializeField] private FishRootJoint _fishRootJoint;

        private Vector3 _direction;
        private float _directionAngle;
        private FishingBoundsService _fishingBoundsService;
        private CoroutineMonoRunner _coroutineMonoRunner;

        private WaitForSeconds _waitForSeconds;
        private Coroutine _moveJob;
        private float _speed;

        private Func<float, bool> _isReachedCondition;

        private bool ReachedLeftBoundX(float current) =>
            current < _fishingBoundsService.Bounds.min.x;

        private bool ReachedRightBoundX(float current) =>
            current > _fishingBoundsService.Bounds.max.x;

        public void Construct(CoroutineMonoRunner coroutineMonoRunner, FishingBoundsService fishingBoundsService)
        {
            _fishingBoundsService = fishingBoundsService;
            _coroutineMonoRunner = coroutineMonoRunner;
        }

        public void Init(Vector3 startPosition, Vector3 direction, float speed)
        {
            _fishRootJoint.SetPosition(startPosition);

            _direction = direction;

            _isReachedCondition = _direction.x < 0
                ? ReachedLeftBoundX
                : ReachedRightBoundX;

            _directionAngle = _direction.x > 0
                ? 90
                : -90;

            _speed = speed * _speedMultiplier;
            _waitForSeconds = new WaitForSeconds(0.333f);
        }

        public void Run()
        {
            _coroutineMonoRunner.Run(Move());
        }

        public void Stop()
        {
            _coroutineMonoRunner.Stop();
            GetComponent<FishPoolComponent>().Release();
        }

        private IEnumerator Move()
        {
            float angle = 10;
            float fishRootJointPositionZ = _fishRootJoint.Position.x;
            
            while (_isReachedCondition.Invoke(fishRootJointPositionZ) == false)
            {
                fishRootJointPositionZ = _fishRootJoint.Position.x;

                Vector3 nextPosition = _fishRootJoint.Position + _speed * _direction.normalized;

                angle *= -1;

                _fishRootJoint.SetPosition(nextPosition);
                _fishRootJoint.SetViewAngle(_directionAngle + angle);

                yield return _waitForSeconds;
            }

            Stop();
        }
    }
}