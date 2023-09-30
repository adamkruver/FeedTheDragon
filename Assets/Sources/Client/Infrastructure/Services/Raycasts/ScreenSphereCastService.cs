using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Raycasts
{
    public class ScreenSphereCastService
    {
        private readonly ScreenPointToRayService _screenPointToRayService;

        private readonly RaycastHit[] _hits = new RaycastHit[100];
        private readonly int _layerMask;

        public ScreenSphereCastService(Camera camera, int layerMask)
        {
            _screenPointToRayService = new ScreenPointToRayService(camera);
            _layerMask = layerMask;
        }

        public bool TryGetComponents<T>(Vector3 pointerPosition, float radius, out T[] components)
            where T : MonoBehaviour
        {
            Ray ray = _screenPointToRayService.GetRay(pointerPosition);

            int hitAmount = Physics.SphereCastNonAlloc(ray, radius, _hits, float.MaxValue, _layerMask);

            components = GetComponents<T>(hitAmount);

            return components.Length != 0;
        }

        public bool CheckCollision<T>(Vector3 pointerPosition, float radius, T component) where T : MonoBehaviour
        {
            if (TryGetComponents(pointerPosition, radius, out T[] components) == false)
                return false;
            
            return components.Contains(component);
        }

        private T[] GetComponents<T>(int hitAmount) where T : MonoBehaviour
        {
            List<T> components = new List<T>();

            for (int i = 0; i < hitAmount; i++)
                if (_hits[i].collider.TryGetComponent(out T component))
                    components.Add(component);

            return components.ToArray();
        }
    }
}