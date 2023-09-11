using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Animators;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class AnimationSpeedViewModelComponent : IViewModelComponent
    {
        private readonly LiveData<float> _speed;

        [PropertyBinding(typeof(IAnimatorFloatPropertyBind), "Speed")]
        private IBindableProperty<float> _animationSpeed;

        public AnimationSpeedViewModelComponent(int id, GetSpeedQuery addSpeedListener) =>
            _speed = addSpeedListener.Handle(id);

        public void Enable() =>
            _speed.Observe(OnSpeedChanged);

        public void Disable() =>
            _speed.Unobserve(OnSpeedChanged);

        private void OnSpeedChanged(float speed) =>
            _animationSpeed.Value = speed;
    }
}