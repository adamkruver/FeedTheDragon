using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Raycasts
{
    public class ScreenPointToRayService
    {
        private readonly Camera _camera;

        public ScreenPointToRayService(Camera camera)
        {
            _camera = camera;
        }

        public Ray GetRay(Vector3 screenPosition) =>
            _camera.ScreenPointToRay(screenPosition);
    }
}