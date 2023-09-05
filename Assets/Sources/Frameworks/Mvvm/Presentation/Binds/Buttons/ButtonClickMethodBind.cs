using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Frameworks.Mvvm.Binds.Buttons
{
    public class ButtonClickMethodBind : BindableViewMethod<Vector3>, IButtonClickMethodBind
    {
        [SerializeField] private Button _button;

        private void OnEnable() =>
            _button?.onClick.AddListener(OnButtonClick);

        private void OnDisable() =>
            _button?.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() =>
            BindingCallback.Invoke(Input.mousePosition);
    }
}