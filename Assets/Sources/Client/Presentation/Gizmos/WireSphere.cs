using UnityEngine;

namespace Sources.Client.Presentation.Gizmos
{
    public class WireSphere : MonoBehaviour
    {
        [SerializeField] float _radius = 5.0f;
        [SerializeField] Color _color = Color.white;

        void OnDrawGizmosSelected()
        {
            UnityEngine.Gizmos.color = _color;
            UnityEngine.Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}