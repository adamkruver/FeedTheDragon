using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Transformso
{
    public class TransformUpPropertyBind : BindableViewProperty<Vector3>, ITransformUpPropertyBind
    {
        public override Vector3 BindableProperty
        {
            get => transform.up;
            set => transform.up = value;
        }
    }
}