using UnityEngine;

namespace Sources.Client.Presentation.Views
{
    public class LookAtCameraMain : MonoBehaviour
    {
        private Transform _transform;
        private Transform _cameraTransform;
        private Camera _camera;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _camera = Camera.main;
            _cameraTransform = _camera.GetComponent<Transform>();
        }

        private void Update()
        {
            _transform.rotation = _cameraTransform.rotation;
        }
    }
}