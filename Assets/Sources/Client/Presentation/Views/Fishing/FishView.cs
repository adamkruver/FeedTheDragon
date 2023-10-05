using System;
using System.Collections;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Presentation.PoolComponents;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishView : MonoBehaviour
    {
        [SerializeField] private FishAnimation _fishAnimation;
        [SerializeField] private float _speedMultiplier = 1f;

        private FishingBoundsService _fishingBoundsService;
        private CoroutineMonoRunner _coroutineMonoRunner;

        private Vector3 _baseDirection;
        private float _baseSpeed;
        private float _maxSpeed = 10;

        private Vector3 _direction;
        private float _speed;

        private Func<float, bool> _isReachedCondition;
        private Transform _transform;

        private bool ReachedLeftBoundX(float current) =>
            current < _fishingBoundsService.Bounds.min.x;

        private bool ReachedRightBoundX(float current) =>
            current > _fishingBoundsService.Bounds.max.x;

        private void Awake() =>
            _transform = GetComponent<Transform>();

        public void Construct(CoroutineMonoRunner coroutineMonoRunner, FishingBoundsService fishingBoundsService)
        {
            _fishingBoundsService = fishingBoundsService;
            _coroutineMonoRunner = coroutineMonoRunner;
        }

        public void Init(Vector3 startPosition, Vector3 direction, float speed)
        {
            SetDirection(direction);
            SetViewDirection(direction);
            _transform.position = startPosition;

            _isReachedCondition = direction.x < 0
                ? ReachedLeftBoundX
                : ReachedRightBoundX;

            _speed = Mathf.Clamp(speed * _speedMultiplier, 1f, _maxSpeed);

            _baseDirection = _direction;
            _baseSpeed = _speed;
        }

        public void Enable() =>
            gameObject.SetActive(true);

        public void Disable() =>
            gameObject.SetActive(false);

        public void SetDirection(Vector3 direction) =>
            _direction = direction;
        
        public void SetViewDirection(Vector3 direction)
        {
            float x = direction.x > 0 ? -1f : 1f;
            _transform.localScale = new Vector3(x, 1f, 1f);
        }

        public void SetSpeed(float speed) =>
            _speed = speed;

        public void Run()
        {
            Enable();
            _coroutineMonoRunner.Run(Move());
        }

        public void Catch()
        {
            //_fishAnimation.Catch();
        }

        public void Swim()
        {
            _fishAnimation.Swim();
            
            SetDirection(_baseDirection);
            SetViewDirection(_baseDirection);
            SetSpeed(_maxSpeed);
        }

        private void ReturnToPool()
        {
            _coroutineMonoRunner.Stop();
            GetComponent<FishPoolComponent>().Release();
            SetViewDirection(_baseDirection);
        }

        private IEnumerator Move()
        {
            while (_isReachedCondition.Invoke(_transform.position.x) == false)
            {
                _transform.position += _speed * Time.deltaTime * _direction;

                yield return null;
            }

            ReturnToPool();
        }
    }
}