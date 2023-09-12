using UnityEngine;

namespace Sources.Client.Presentation.Views
{
    public class LookAtCameraMain : MonoBehaviour
    {
        private Transform _transform;
        private Transform _cameraTransform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _cameraTransform = Camera.main.GetComponent<Transform>();
        }

        private void Update()
        {
            _transform.rotation = _cameraTransform.rotation;
        }
    }
}