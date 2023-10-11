using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Raycasts
{
    public class ScreenRaycastService
    {
        private readonly ScreenPointToRayService _screenPointToRayService;
        private readonly int _layerMask;

        public ScreenRaycastService(Camera camera, int layerMask)
        {
            _screenPointToRayService = new ScreenPointToRayService(camera);
            _layerMask = layerMask;
        }
        
        public bool TryRaycast(Vector3 screenPosition, out RaycastHit hit)
        {
            Ray ray = _screenPointToRayService.GetRay(screenPosition);
            
            return Physics.Raycast(ray, out hit, float.MaxValue, _layerMask);
        }
    }
}