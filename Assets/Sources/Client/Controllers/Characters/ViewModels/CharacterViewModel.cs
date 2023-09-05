using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain.Characters;
using Sources.Client.Presentation.Binds.Common;
using Sources.Client.PresentationInterfaces.Binds.CharacterController;
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

        public CharacterViewModel(IViewModelComponent[] components, Character character) : base(components)
        {
            _character = character;
        }

        protected override void OnEnable()
        {
            _isEnabled.Value = true;
            
            _character.Position.Changed += OnPositionChanged;
            _character.LookDirection.Changed += OnLookDirectionChanged;
        }

        protected override void OnDisable()
        {
            _character.Position.Changed -= OnPositionChanged;
            _character.LookDirection.Changed -= OnLookDirectionChanged;
        }

        private void OnLookDirectionChanged()
        {
            _worldRotation.Value = _character.LookDirection.Value;
        }

        private void OnPositionChanged()
        {
            _controllerPosition.Value = _character.Position.Value - _transformPosition.Value;
            
            _character.Position.Set(_transformPosition.Value);
        }
    }
}