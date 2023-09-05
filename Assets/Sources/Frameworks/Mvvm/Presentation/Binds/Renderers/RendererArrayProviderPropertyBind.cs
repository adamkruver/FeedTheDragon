using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Renderers;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Renderers
{
    public class RendererArrayProviderPropertyBind : BindableViewProperty<Renderer[]>,
        IRendererArrayProviderPropertyBind
    {
        [SerializeField] private Renderer[] _renderers;

        public override Renderer[] BindableProperty
        {
            get => _renderers;
            set { }
        }
    }
}