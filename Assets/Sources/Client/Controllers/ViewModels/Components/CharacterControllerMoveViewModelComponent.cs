using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class CharacterControllerMoveViewModelComponent : IViewModelComponent
    {
        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;
        
        [PropertyBinding(typeof(ICharacterControllerMovePropertyBind))]
        private IBindableProperty<Vector3> _controllerMovement;
        
        [PropertyBinding(typeof(ICharacterControllerPositionPropertyBind))]
        private IBindableProperty<Vector3> _controllerPosition;

        public CharacterControllerMoveViewModelComponent(IComposite composite)
        {
            if (composite.TryGetComponent(out PositionComponent positionComponent) == false)
                throw new NullReferenceException();

            Position = positionComponent;
        }

        private PositionComponent Position { get; }

        public void Enable()
        {
            Position.Changed += OnPositionChanged;
            _controllerPosition.Value = Position.Value;
            OnPositionChanged();
        }

        public void Disable()
        {
            Position.Changed -= OnPositionChanged;
        }

        private void OnPositionChanged()
        {
            _controllerMovement.Value = Position.Value - _transformPosition.Value;

            Vector3 position = _transformPosition.Value;
            position.y = 0;

            Position.Set(position);
        }
    }
}