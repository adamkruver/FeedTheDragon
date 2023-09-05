using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Transforms
{
    public class TransformForwardPropertyBind : BindableViewProperty<Vector3>,ITransformForwardPropertyBind
    {
        public override Vector3 BindableProperty
        {
            get => transform.forward;
            set => transform.forward = value;
        }
    }
}