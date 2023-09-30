using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Cameras;
using Sources.Client.Presentation.Cameras.Types;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Cameras
{
    public class CameraFollowService : ICameraFollowService
    {
        private readonly Transform _camera;
        
        private Transform _target;

        public CameraFollowService(ICameraProvider cameraProvider)
        {
            _camera = cameraProvider.Get<MainCamera>().transform.parent;
        }
        
        public void Follow(Transform target)
        {
            _target = target;
        }

        public void LateUpdate(float deltaTime)
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
