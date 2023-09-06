using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Animators;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class AnimationSpeedViewModelComponent : IViewModelComponent
    {
        [PropertyBinding(typeof(IAnimatorFloatPropertyBind), "Speed")]
        private IBindableProperty<float> _animationSpeed;

        public AnimationSpeedViewModelComponent(IComposite composite)
        {
            if (composite.TryGetComponent(out SpeedComponent speedComponent) == false)
                throw new NullReferenceException();

            Speed = speedComponent;
        }

        private SpeedComponent Speed { get; }

        public void Enable()
        {
            Speed.Changed += OnSpeedChanged;
            OnSpeedChanged();
        }

        public void Disable()
        {
            Speed.Changed -= OnSpeedChanged;
        }

        private void OnSpeedChanged()
        {
            _animationSpeed.Value = Speed.Value;
        }
    }
}