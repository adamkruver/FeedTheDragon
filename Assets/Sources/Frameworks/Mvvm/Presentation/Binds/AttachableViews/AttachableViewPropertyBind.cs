using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Sources.Frameworks.Mvvm.PresentationInterfaces.Binds.AttachableViews;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Sources.Frameworks.Mvvm.Presentation.Binds.AttachableViews
{
    public class AttachableViewPropertyBind : BindableViewProperty<IAttachableView>, IAttachableView,
        IAttachableViewPropertyBind
    {
        public override IAttachableView BindableProperty
        {
            get => this;
            set { return; }
        }

        public void Attach(IBindableView bindableView) => 
            ((MonoBehaviour)bindableView).transform.SetParent(transform, false);
    }
}