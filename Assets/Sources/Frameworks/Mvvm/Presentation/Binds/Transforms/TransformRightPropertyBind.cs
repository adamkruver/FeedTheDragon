using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Transforms
{
    public class TransformRightPropertyBind : BindableViewProperty<Vector3>, ITransformRightPropertyBind
    {
        public override Vector3 BindableProperty
        {
            get => transform.right;
            set => transform.right = value;
        }
    }
}