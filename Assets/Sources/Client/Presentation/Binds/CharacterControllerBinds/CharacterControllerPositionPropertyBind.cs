using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.CharacterControllerBinds
{
    public class CharacterControllerPositionPropertyBind : BindableViewProperty<Vector3>,
        ICharacterControllerPositionPropertyBind
    {
        [SerializeField] private CharacterController _characterController;

        private Transform _transform;

        private Transform Transform => _transform ??= _characterController.GetComponent<Transform>();
        
        public override Vector3 BindableProperty
        {
            get => _transform.position;
            set
            {
                _characterController.enabled = false;
                Transform.position = value;
                _characterController.enabled = true;
            }
        }
    }
}