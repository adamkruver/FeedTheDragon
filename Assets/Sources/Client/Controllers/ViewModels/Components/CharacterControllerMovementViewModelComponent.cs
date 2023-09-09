using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using UnityEngine;
using Utils.LiveData;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class CharacterControllerMovementViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly SetPositionCommand _setPositionCommand;
        private LiveData<Vector3> _position;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;

        [PropertyBinding(typeof(ICharacterControllerMovePropertyBind))]
        private IBindableProperty<Vector3> _controllerMovement;

        [PropertyBinding(typeof(ICharacterControllerPositionPropertyBind))]
        private IBindableProperty<Vector3> _controllerPosition;

        public CharacterControllerMovementViewModelComponent
            (int id, GetPositionQuery getPositionQuery, SetPositionCommand setPositionCommand)
        {
            _id = id;
            _setPositionCommand = setPositionCommand;
            _position = getPositionQuery.Handle(id);
        }

        public void Enable()
        {
            _transformPosition.Value = _position.Value;
            _position.Observe(OnPositionChanged);
        }

        public void Disable()
        {
            _position.Unobserve(OnPositionChanged);
        }

        private void OnPositionChanged(Vector3 position)
        {
            _controllerMovement.Value = position - _transformPosition.Value;

            _setPositionCommand.Handle(_id, _controllerMovement.Value);
        }
    }
}