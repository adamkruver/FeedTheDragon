using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishRootJoint : MonoBehaviour
    {
        private Transform _transform;

        public Vector3 Position => _transform.position;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void SetViewAngle(float angle)
        {
            _transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
    }
}