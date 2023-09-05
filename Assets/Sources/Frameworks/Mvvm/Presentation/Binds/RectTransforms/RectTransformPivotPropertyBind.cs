using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.RectTransforms;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.RectTransforms
{
    [RequireComponent(typeof(RectTransform))]
    public class RectTransformPivotPropertyBind : BindableViewProperty<Vector2>,IRectTransformPivotPropertyBind
    {
        private RectTransform _rectTransform;

        private void Awake() => 
            _rectTransform = GetComponent<RectTransform>();

        public override Vector2 BindableProperty
        {
            get => _rectTransform.pivot;
            set => _rectTransform.pivot = value;
        }
    }
}