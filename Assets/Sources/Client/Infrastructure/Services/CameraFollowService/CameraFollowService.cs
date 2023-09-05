using UnityEngine;

namespace Sources.Client.CameraFollower
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
            
            _camera.transform.position = _target.position;
        }
    }
}
