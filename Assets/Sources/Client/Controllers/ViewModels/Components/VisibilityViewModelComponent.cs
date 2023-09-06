using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain;
using Sources.Client.Domain.Components;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class VisibilityViewModelComponent : IViewModelComponent
    {
        [PropertyBinding(typeof(IBindableViewEnabledPropertyBind))]
        private IBindableProperty<bool> _isEnabled;

        public VisibilityViewModelComponent(IComposite composite)
        {
            if (composite.TryGetComponent(out VisibilityComponent visibilityComponent) == false)
                throw new NullReferenceException();

            Visibility = visibilityComponent;
        }

        private VisibilityComponent Visibility { get; }

        public void Enable()
        {
            Visibility.Changed += OnVisibilityChanged;
            OnVisibilityChanged();
        }

        public void Disable()
        {
            Visibility.Changed += OnVisibilityChanged;
        }

        private void OnVisibilityChanged()
        {
            _isEnabled.Value = Visibility.IsVisible;
        }
    }
}