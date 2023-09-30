using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    public class FishingSightSphereCaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private int _maxColliders = 100;

        private RaycastHit[] _hits;

        private void Awake()
        {
            _hits = new RaycastHit[_maxColliders];
        }

        public bool TryGetFish(Vector3 screenPosition, float radius, out FishView fishView)
        {
            fishView = null;

            Ray ray = _camera.ScreenPointToRay(screenPosition);

            int colliders = Physics.SphereCastNonAlloc(
                ray,
                radius,
                _hits,
                Mathf.Infinity,
                1 << LayerMask.NameToLayer("Interactable")
            );

            if (colliders == 0)
                return false;

            fishView = GetFish(colliders, radius);

            return fishView != null;
        }

        public bool HasCollision(Vector3 screenPosition, FishView fish, float radius)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            int colliders = Physics.SphereCastNonAlloc(
                ray,
                radius,
                _hits,
                Mathf.Infinity,
                1 << LayerMask.NameToLayer("Interactable")
            );

            if (colliders == 0)
                return false;

            return HasFish(colliders, fish);
        }

        private FishView GetFish(int colliders, float radius)
        {
            for (int i = 0; i < colliders; i++)
            {
                Collider target = _hits[i].collider;

                if (target.TryGetComponent(out FishView fishView))
                {
                    return fishView;
                }
            }

            return null;
        }
        
        private bool HasFish(int colliders, FishView fish)
        {
            for (int i = 0; i < colliders; i++)
            {
                Collider target = _hits[i].collider;

                if (target.TryGetComponent(out FishView fishView))
                {
                    if(fishView == fish)
                        return true;
                }
            }

            return false;
        }
    }
}