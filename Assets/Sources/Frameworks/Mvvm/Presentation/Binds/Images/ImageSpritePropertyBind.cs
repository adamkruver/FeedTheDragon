using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Images;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Frameworks.Mvvm.Binds.Images
{
    public class ImageSpritePropertyBind : BindableViewProperty<Sprite>, IImageSpritePropertyBind
    {
        [SerializeField] private Image _image;

        public override Sprite BindableProperty
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }
    }
}