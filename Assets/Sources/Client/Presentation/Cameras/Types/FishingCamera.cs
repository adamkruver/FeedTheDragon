using UnityEngine;

namespace Sources.Client.Presentation.Cameras.Types
{
    public class FishingCamera : CameraType
    {
        private readonly float _divider = 2.5f;
        private readonly float _epsilon = .01f;

        private Transform _transform;
        
        private float _screenWidth = -1f;
        private float _screenHeight = -1f;
        
        public bool IsScreenSizeChanged => Mathf.Abs(_screenWidth - Screen.width) > _epsilon
                                            || Mathf.Abs(_screenHeight - Screen.height) > _epsilon;
        protected override sealed void OnAwake() =>
            _transform = GetComponent<Transform>();

        public void ChangeWidth(Vector3 leftTopPoint, Vector3 rightTopPoint)
        {
            Vector3 position = _transform.localPosition;
            
            position.x = Vector3.Distance(leftTopPoint, rightTopPoint) / _divider;
            _transform.localPosition = position;
        }

        public void SetActualSize()
        {
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
        }
    }
}