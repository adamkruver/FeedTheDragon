using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Scales;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class CharacterTriggerVisibilitySwitchViewModelComponent : IViewModelComponent
    {
        [PropertyBinding(typeof(IGameObjectEnableWithScaleFadePropertyBind))]
        private IBindableProperty<bool> _isProximityEnabled;

        public void Enable()
        {
        }

        public void Disable()
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
    }
}