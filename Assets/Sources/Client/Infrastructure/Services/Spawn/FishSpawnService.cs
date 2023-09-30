using System.Collections;
using Sources.Client.Domain;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Spawn
{
    public class FishSpawnService : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private FishView[] _prefabs;

        private Coroutine _spawnJob;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            Run();
        }

        public void Run()
        {
            Stop();

            _spawnJob = StartCoroutine(Spawn());
        }

        public void Stop()
        {
            if (_spawnJob == null)
                return;

            StopCoroutine(_spawnJob);
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                CreateFish();
                yield return new WaitForSeconds(Random.Range(.1f, 2f));
            }
        }

        private void CreateFish()
        {
            FishView fishPrefab = _prefabs[Random.Range(0, _prefabs.Length)];

            FishView fishView = Instantiate(fishPrefab, _transform);

            (Vector3 leftPosition, Vector3 rightPosition) = CreatePositions(Random.Range(0f, 1f));

            Direction direction = GetRandomDirection();

            if (direction == Direction.Left)
                (leftPosition, rightPosition) = (rightPosition, leftPosition);

            fishView
                .SetStartPoint(leftPosition)
                .SetEndPoint(rightPosition)
                //         .SetDirection(direction)
                .SetSpeed(Random.Range(2.5f, 4.5f))
                .Run();
        }

        private Direction GetRandomDirection()
        {
            return Random.Range(0, 2) == 0
                ? Direction.Left
                : Direction.Right;
        }

        private (Vector3 leftPosition, Vector3 rightPosition) CreatePositions(float depth)
        {
            float halfScreenHeight = Screen.height / 3f;
            float fishDepth = halfScreenHeight * depth + Screen.height / 30f;

            Vector3 leftScreenPosition = new Vector3(-200, fishDepth, 0);
            Vector3 rightScreenPosition = new Vector3(Screen.width + 200, fishDepth, 0);

            return (RaycastFromScreenPosition(leftScreenPosition), RaycastFromScreenPosition(rightScreenPosition));
        }

        private Vector3 RaycastFromScreenPosition(Vector3 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,
                1 << LayerMask.NameToLayer("TransparentFX"));

            return hit.point;
        }
    }
}