using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Renderers;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Renderers
{
    public class RendererPropertyBind : BindableViewProperty<Renderer>, IRendererPropertyBind
    {
        [SerializeField] private Renderer _renderer;

        public override Renderer BindableProperty
        {
            get => _renderer;
            set { }
        }
    }
}