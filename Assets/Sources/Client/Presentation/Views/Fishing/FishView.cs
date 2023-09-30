using System.Collections;
using Sources.Client.Domain;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishView : MonoBehaviour
    {
        [SerializeField] private float _speedMultiplier = 1f;
        [SerializeField] private FishRootJoint _fishRootJoint;
        [SerializeField] private Transform _transform;

        private WaitForSeconds _waitForSeconds;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Coroutine _moveJob;
        private float _speed;

        public FishView SetStartPoint(Vector3 position)
        {
            _fishRootJoint.SetStartPosition(position);
            _transform.position = position;
            _startPosition = position;

            return this;
        }

        public FishView SetEndPoint(Vector3 position)
        {
            _endPosition = position;

            return this;
        }

        public FishView SetSpeed(float speed)
        {
            _speed = speed * _speedMultiplier;
            _waitForSeconds = new WaitForSeconds(_speed/10);

            return this;
        }

        public void Run()
        {
            Stop();
            gameObject.SetActive(true);

            _moveJob = StartCoroutine(Move());
        }

        public void Stop()
        {
            if (_moveJob == null)
                return;

            StopCoroutine(_moveJob);
            gameObject.SetActive(false);
        }

        private IEnumerator Move()
        {
            Vector3 direction;
            Vector3 currentPosition;

            bool even = false;

            while (Vector3.Distance(_fishRootJoint.Position, _endPosition) > 2f)
            {
                currentPosition = _fishRootJoint.Position;
                direction = _endPosition - currentPosition;

                Vector3 nextPosition = currentPosition + (_speed/4) * direction.normalized ;

                nextPosition += even ? Vector3.forward / 6 : -Vector3.forward / 6;

                even = !even;
                
                _fishRootJoint.SetPosition(nextPosition);
                
                yield return _waitForSeconds;
            }

            Stop();
        }
    }
}