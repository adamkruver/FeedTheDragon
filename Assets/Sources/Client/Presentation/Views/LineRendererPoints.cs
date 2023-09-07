using UnityEngine;

namespace Sources.Client.Presentation.Views
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.positionCount = _points.Length;
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _points.Length; i++)
            {
                Vector3 position = _points[i].position;
                _lineRenderer.SetPosition(i, position);
            }
        }
    }
}