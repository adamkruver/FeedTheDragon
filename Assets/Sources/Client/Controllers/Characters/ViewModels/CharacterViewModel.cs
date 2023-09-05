using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Transforms;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain.Characters;
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

        public CharacterViewModel(IViewModelComponent[] components, Character character) : base(components)
        {
            _character = character;
        }

        protected override void OnEnable()
        {
            _isEnabled.Value = true;
            
            _character.Position.Changed += OnPositionChanged;
        }

        protected override void OnDisable()
        {
            _character.Position.Changed -= OnPositionChanged;
        }

        private void OnPositionChanged()
        {
            _controllerPosition.Value = _character.Position.CurrentPosition - _transformPosition.Value;
        }
    }
}