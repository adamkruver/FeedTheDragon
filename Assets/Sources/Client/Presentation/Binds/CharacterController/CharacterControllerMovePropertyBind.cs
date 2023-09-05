using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.CharacterController
{
    public class CharacterControllerMovePropertyBind : BindableViewProperty<Vector3>, ICharacterControllerMovePropertyBind
    {
        [SerializeField] private UnityEngine.CharacterController _characterController;
        
        public override Vector3 BindableProperty
        {
            get => default;
            set => _characterController.Move(value);
        }
    }
}