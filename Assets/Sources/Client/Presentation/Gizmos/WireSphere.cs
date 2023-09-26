using UnityEngine;

namespace Sources.Editor
{
    public class WireSphere : MonoBehaviour
    {
        [SerializeField] float _radius = 5.0f;
        [SerializeField] Color _color = Color.white;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = _color;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}