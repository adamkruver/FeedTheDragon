using System.Collections;
using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.Scales;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.Scales
{
    public class GameObjectEnableWithScaleFadePropertyBind : BindableViewProperty<bool>,
        IGameObjectEnableWithScaleFadePropertyBind
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _isEnabledOnStart = false;
        [SerializeField] private float _scaleSpeed = 1f;

        private const float Epsilon = 0.01f;
        
        private Transform _transform;
        private Coroutine _scaleJob;
        private float _scale = 0;
        private bool _isEnabled;

        private Transform Transform => _transform ??= _gameObject.GetComponent<Transform>();

        private void Start()
        {
            BindableProperty = _isEnabledOnStart;
            Transform.localScale = Vector3.zero;
        }

        public override bool BindableProperty
        {
            get => _isEnabled;
            set => SetScale(value ? 1 : 0);
        }

        private void Enable()
        {
            if (_isEnabled)
                return;

            _gameObject.SetActive(true);
            _isEnabled = true;
        }

        private void Disable()
        {
            if (_isEnabled == false)
                return;

            _gameObject.SetActive(false);
            _isEnabled = false;
        }

        private void SetScale(float scale)
        {
            StopScale();
            Enable();            
            RunScale(scale);
        }

        private void StopScale()
        {
            if (_scaleJob == null)
                return;

            StopCoroutine(_scaleJob);

            if (Mathf.Abs(_scale) <= Epsilon)
                Disable();
            else
                Enable();
        }

        private void RunScale(float scale)
        {
            _scaleJob = StartCoroutine(ScaleCoroutine(scale));
        }

        private IEnumerator ScaleCoroutine(float scale)
        {
            do
            {
                _scale = Mathf.Lerp(_scale, scale, _scaleSpeed * Time.deltaTime);
                Transform.localScale = _scale * Vector3.one;
                
                yield return null;
            } while (Mathf.Abs(_scale - scale) > Epsilon);
            
            StopScale();
        }
    }
}