using System;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraPositionHolder : MonoBehaviour
    {
        [SerializeField] private float _divider = 10f;
        private readonly float _epsilon = .01f;

        private Transform _transform;
        private float _cameraDistance;
        private Camera _camera;

        private int _layer;
        private float _screenWidth = -1f;
        private float _screenHeight = -1f;

        private bool IsScreenSizeChanged => Mathf.Abs(_screenWidth - Screen.width) > _epsilon
                                        || Mathf.Abs(_screenHeight - Screen.height) > _epsilon;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _transform = GetComponent<Transform>();

            _layer = 1 << LayerMask.NameToLayer("TransparentFX");
        }

        public void LateUpdate()
        {
            if (IsScreenSizeChanged == false)
                return;

            _screenWidth = Screen.width;
            _screenHeight = Screen.height;

            Ray leftTopCornerRay = _camera.ScreenPointToRay(Vector3.zero);
            Ray rightTopCornerRay = _camera.ScreenPointToRay(new Vector3(_screenWidth, 0, 0));

            Physics.Raycast(leftTopCornerRay, out RaycastHit leftRaycastHit, Mathf.Infinity, _layer);
            Physics.Raycast(rightTopCornerRay, out RaycastHit rightRaycastHit, Mathf.Infinity, _layer);

            Vector3 position = _transform.localPosition;
            position.x = Vector3.Distance(leftRaycastHit.point, rightRaycastHit.point) / _divider;
            _transform.localPosition = position;
        }
    }
}