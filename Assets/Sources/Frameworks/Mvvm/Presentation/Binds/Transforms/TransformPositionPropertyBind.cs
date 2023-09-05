using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Transforms
{
    public class TransformPositionPropertyBind : BindableViewProperty<Vector3>, ITransformPositionPropertyBind
    {
        public override Vector3 BindableProperty
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}