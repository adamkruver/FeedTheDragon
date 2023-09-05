using UnityEngine;

namespace Sources.Camera
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
    
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            _transform.position = _target.position;
        }
    }
}
