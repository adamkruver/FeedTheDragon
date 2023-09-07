using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Animators;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Listeners;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class AnimationSpeedViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddSpeedListener _addSpeedListener;
        private readonly RemoveSpeedListener _removeSpeedListener;
        private readonly GetSpeedQuery _getSpeedQuery;

        [PropertyBinding(typeof(IAnimatorFloatPropertyBind), "Speed")]
        private IBindableProperty<float> _animationSpeed;

        public AnimationSpeedViewModelComponent
        (
            int id,
            AddSpeedListener addSpeedListener,
            RemoveSpeedListener removeSpeedListener,
            GetSpeedQuery getSpeedQuery
        )
        {
            _id = id;
            _addSpeedListener = addSpeedListener;
            _removeSpeedListener = removeSpeedListener;
            _getSpeedQuery = getSpeedQuery;
        }

        public void Enable()
        {
            _addSpeedListener.Handle(_id, OnSpeedChanged);
            OnSpeedChanged();
        }

        public void Disable()
        {
            _removeSpeedListener.Handle(_id, OnSpeedChanged);
        }

        private void OnSpeedChanged()
        {
            _animationSpeed.Value = _getSpeedQuery.Handle(_id);
        }
    }
}