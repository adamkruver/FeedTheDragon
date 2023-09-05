using Domain.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.TextMeshPros;
using TMPro;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.TextMeshPros
{
    public class TextMeshProTextPropertyBind : BindableViewProperty<string>, ITextMeshProTextPropertyBind
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        public override string BindableProperty
        {
            get => _textMesh.text;
            set => _textMesh.text = value;
        }
    }
}