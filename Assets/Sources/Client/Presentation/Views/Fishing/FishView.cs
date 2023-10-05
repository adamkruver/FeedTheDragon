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
        [SerializeField] private float _speedMultiplier = 1f;

        private Vector3 _direction;
        private FishingBoundsService _fishingBoundsService;
        private CoroutineMonoRunner _coroutineMonoRunner;

        private float _speed;

        private Func<float, bool> _isReachedCondition;
        private Transform _transform;

        private bool ReachedLeftBoundX(float current) =>
            current < _fishingBoundsService.Bounds.min.x;

        private bool ReachedRightBoundX(float current) =>
            current > _fishingBoundsService.Bounds.max.x;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void Construct(CoroutineMonoRunner coroutineMonoRunner, FishingBoundsService fishingBoundsService)
        {
            _fishingBoundsService = fishingBoundsService;
            _coroutineMonoRunner = coroutineMonoRunner;
        }

        public void Init(Vector3 startPosition, Vector3 direction, float speed)
        {
            _direction = direction;
            _transform.right = -direction;
            _transform.position = startPosition;
            
            _isReachedCondition = direction.x < 0
                ? ReachedLeftBoundX
                : ReachedRightBoundX;

            _speed = Mathf.Clamp(speed * _speedMultiplier, 1f, 10f);
        }

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);

        public void Run()
        {
            Enable();
            _coroutineMonoRunner.Run(Move());
        }

        public void Stop()
        {
            _coroutineMonoRunner.Stop();
            GetComponent<FishPoolComponent>().Release();
        }

        private IEnumerator Move()
        {
            while (_isReachedCondition.Invoke(_transform.position.x) == false)
            {
                _transform.position += _speed * Time.deltaTime * _direction.normalized ;
                
                yield return null;
            }

            Stop();
        }
    }
}