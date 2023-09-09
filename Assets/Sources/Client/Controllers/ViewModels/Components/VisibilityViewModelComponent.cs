using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.Common.Components.Visibilities.Queries;
using Utils.LiveData;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class VisibilityViewModelComponent : IViewModelComponent
    {
        private readonly LiveData<bool> _isVisible;

        [PropertyBinding(typeof(IBindableViewEnabledPropertyBind))]
        private IBindableProperty<bool> _isEnabled;

        public VisibilityViewModelComponent(int id, GetVisibilityQuery getVisibilityQuery) =>
            _isVisible = getVisibilityQuery.Handle(id);

        public void Enable() =>
            _isVisible.Observe(OnVisibilityChanged);

        public void Disable() =>
            _isVisible.Unobserve(OnVisibilityChanged);

        private void OnVisibilityChanged(bool value) =>
            _isEnabled.Value = value;
    }
}