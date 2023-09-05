using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.Rotations;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.Rotations
{
    public class LookDirectionPropertyBind : BindableViewProperty<Vector3>, ILookDirectionPropertyBind
    {
        [SerializeField] private Transform _transform;

        public override Vector3 BindableProperty
        {
            get => _transform.forward; 
            set => _transform.forward = value;
        }
    }
}