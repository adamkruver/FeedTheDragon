using System;
using UnityEngine;

namespace Sources.Client.Presentation.Cameras
{
    public abstract class CameraType : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [field: SerializeField] public Camera Camera { get; private set; }

        private void Awake() =>
            OnAwake();

        protected virtual void OnAwake()
        {}

        public void Enable()
        {
            Camera.enabled = true;

            if (_canvas)
                _canvas.gameObject.SetActive(true);
        }

        public void Disable()
        {
            Camera.enabled = false;
            
            if (_canvas)
                _canvas.gameObject.SetActive(false);
        }
    }
}