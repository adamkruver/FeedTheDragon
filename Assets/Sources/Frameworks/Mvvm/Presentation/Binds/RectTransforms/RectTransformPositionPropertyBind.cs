using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.RectTransforms;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.RectTransforms
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPositionPropertyBind : BindableViewProperty<Vector3>, IRectTransformPositionPropertyBind
    {
        private RectTransform _rectTransform;

        private Vector3 _position;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        private void Update() =>
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _position, Time.deltaTime * 100);

        public override Vector3 BindableProperty
        {
            get => _rectTransform.position;
            set => _position = value;
        }
    }
}