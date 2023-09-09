using Domain.Frameworks.Mvvm.Properties;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using UnityEngine;

namespace Sources.Client.Presentation.Binds.CharacterControllerBinds
{
    public class CharacterControllerMovePropertyBind : BindableViewProperty<Vector3>,
        ICharacterControllerMovePropertyBind
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private LayerMask _layerMask;

        private Transform _transform;

        public override Vector3 BindableProperty
        {
            get => ProjectOnTerrain();
            set => _characterController.Move(value);
        }

        private Transform Transform => _transform ??= _characterController.GetComponent<Transform>();

        private Vector3 ProjectOnTerrain() =>
            Physics.Raycast(Transform.position, Vector3.down, out RaycastHit hit, 100, _layerMask) == false 
                ? Transform.position 
                : hit.point;
    }
}