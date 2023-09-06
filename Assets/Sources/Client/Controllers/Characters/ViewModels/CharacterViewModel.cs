using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Animators;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Components;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
using Sources.Client.PresentationInterfaces.Binds.Rotations;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        private readonly Character _character;

        [PropertyBinding(typeof(IBindableViewEnabledPropertyBind))]
        private IBindableProperty<bool> _isEnabled;

        [PropertyBinding(typeof(ITransformPositionPropertyBind))]
        private IBindableProperty<Vector3> _transformPosition;

        [PropertyBinding(typeof(ICharacterControllerMovePropertyBind))]
        private IBindableProperty<Vector3> _controllerPosition;

        [PropertyBinding(typeof(ILookDirectionPropertyBind))]
        private IBindableProperty<Vector3> _worldRotation;

        [PropertyBinding(typeof(IAnimatorFloatPropertyBind), "Speed")]
        private IBindableProperty<float> _animationSpeed;

        public CharacterViewModel(IViewModelComponent[] components, Character character) : base(components)
        {
            _character = character;
        }

        private SpeedComponent CharacterSpeed => GetComponent<SpeedComponent>(_character);
        private PositionComponent CharacterPosition => GetComponent<PositionComponent>(_character);
        private LookDirectionComponent CharacterLookDirection => GetComponent<LookDirectionComponent>(_character);

        protected override void OnEnable()
        {
            _isEnabled.Value = true;

            CharacterPosition.Changed += OnPositionChanged;
            CharacterLookDirection.Changed += OnLookDirectionChanged;
            CharacterSpeed.Changed += OnSpeedChanged;
        }

        protected override void OnDisable()
        {
            CharacterPosition.Changed -= OnPositionChanged;
            CharacterLookDirection.Changed -= OnLookDirectionChanged;
            CharacterSpeed.Changed -= OnSpeedChanged;

            _isEnabled.Value = false;
        }

        private void OnLookDirectionChanged()
        {
            _worldRotation.Value = CharacterLookDirection.Value;
        }

        private void OnPositionChanged()
        {
            _controllerPosition.Value = CharacterPosition.Value - _transformPosition.Value;

            Vector3 position = _transformPosition.Value;
            position.y = 0;

            CharacterPosition.Set(position);
        }

        private void OnSpeedChanged()
        {
            _animationSpeed.Value = CharacterSpeed.Value;
        }

        private T GetComponent<T>(Composite composite) where T : IComponent
        {
            if (composite.TryGetComponent(out T component) == false)
                throw new NullReferenceException();

            return component;
        }
    }
}