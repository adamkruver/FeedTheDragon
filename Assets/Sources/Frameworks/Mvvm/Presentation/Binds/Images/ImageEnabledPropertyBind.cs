using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Images;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Frameworks.Mvvm.Binds.Images
{
    public class ImageEnabledPropertyBind : BindableViewProperty<bool>, IImageEnabledPropertyBind
    {
        [SerializeField] private Image _image;

        public override bool BindableProperty
        {
            get => _image.enabled;
            set => _image.enabled = value;
        }
    }
}