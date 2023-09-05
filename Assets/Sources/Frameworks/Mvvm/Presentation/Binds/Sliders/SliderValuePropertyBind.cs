using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Sliders;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Frameworks.Mvvm.Binds.Sliders
{
    public class SliderValuePropertyBind : BindableViewProperty<float>, ISliderValuePropertyBind
    {
        [SerializeField] private Slider _slider;

        public override float BindableProperty
        {
            get => _slider.value;
            set => _slider.value = value;
        }
    }
}