using Domain.Frameworks.Mvvm.Properties;
using Presentation.Frameworks.Mvvm.Views;
using PresentationInterfaces.Frameworks.Mvvm.Binds.BindableViews;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.BindableViews
{
    public class BindableViewEnabledPropertyBind : BindableViewProperty<bool>, IBindableViewEnabledPropertyBind
    {
        [SerializeField] private BindableView _view;

        public override bool BindableProperty
        {
            get => _view.gameObject.activeSelf;
            set => _view.gameObject.SetActive(value);
        }
    }
}