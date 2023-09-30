using System.Linq;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Fishing
{
    [RequireComponent(typeof(LineRenderer))]
    public class FishingLine : MonoBehaviour
    {
        [SerializeField] private Transform _startPointTransform;
        [SerializeField] private Transform _endPointTransform;
        [SerializeField] private Transform _waterlineTransform;
        
        private LineRenderer _lineRenderer;
        private Transform[] _points;

        private void Awake()
        {
            
            _lineRenderer = GetComponent<LineRenderer>();

            _points = new Transform[]
            {
                _startPointTransform,
                _waterlineTransform,
                _endPointTransform,
            };

            _startPointTransform.GetComponent<ConfigurableJoint>().connectedBody =
                _waterlineTransform.GetComponent<Rigidbody>();

            _waterlineTransform.GetComponent<ConfigurableJoint>().connectedBody =
                _endPointTransform.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _lineRenderer.positionCount = _points.Length;
            _lineRenderer.SetPositions(_points.Select(point => point.position).ToArray());
        }

        public void SetEndPoint(Vector3 endPoint)
        {
            _endPointTransform.position = endPoint;
        }
    }
}