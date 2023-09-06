using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.GameObjects;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Mouses;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain.Ingredients;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        [PropertyBinding(typeof(IGameObjectEnabledPropertyBind))]
        private IBindableProperty<bool> _isProximityEnabled;

        public IngredientViewModel(IViewModelComponent[] components) : base(components)
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }

        [MethodBinding(typeof(ITriggerEnterMethodBind))]
        private void OnTriggerEnter(Component other)
        {
            if (other.TryGetComponent<CharacterController>(out _))
                _isProximityEnabled.Value = true;
        }

        [MethodBinding(typeof(ITriggerExitMethodBind))]
        private void OnTriggerExit(Component other)
        {
            if (other.TryGetComponent<CharacterController>(out _))
                _isProximityEnabled.Value = false;
        }

        [MethodBinding(typeof(IMouseDownMethodBind))]
        private void OnClick(Vector3 position)
        {
            Debug.Log(position + " clicked!");
        }
    }
}