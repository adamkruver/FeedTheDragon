using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.CharacterControllerBinds
{
    public class CharacterControllerMovePropertyBind : BindableViewProperty<Vector3>, ICharacterControllerMovePropertyBind
    {
        [SerializeField] private CharacterController _characterController;
        
        public override Vector3 BindableProperty
        {
            get => default;
            set => _characterController.Move(value);
        }
    }
}