using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.Presentation.Cameras;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Terrains
{
    public class TerrainService
    {
        private readonly Camera _camera;
        private readonly int _layer;

        public TerrainService(ICameraProvider cameraProvider)
        {
            _camera = cameraProvider.Get<MainCamera>();
            _layer = 1 << LayerMask.NameToLayer("Terrain"); //todo Move to constants
        }

        public bool TryGetRaycastHit(Vector3 screenPosition, out Vector3 hitPoint)
        {
            hitPoint = default;
            
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _layer) == false)
                return false;

            hitPoint = raycastHit.point;
            
            return true;
        }
    }
}