using UnityEngine;

namespace Sources.Client.Infrastructure.Services.CameraFollowService
{
    public class CameraFollowService
    {
        private readonly Transform _camera;
        
        private Transform _target;

        public CameraFollowService(Transform camera)
        {
            _camera = camera;
        }
        
        public void Follow(Transform target)
        {
            _target = target;
        }

        public void LateUpdate()
        {
            if (_target == null)
                return;

            if (_camera == null)
            {
                Debug.Log("Camera null");
                return;
            }
            
            _camera.transform.position = _target.position;
        }
    }
}
