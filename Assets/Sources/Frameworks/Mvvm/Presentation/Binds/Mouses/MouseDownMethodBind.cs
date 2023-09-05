using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Mouses;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Mouses
{
    public class MouseDownMethodBind : BindableViewMethod<Vector3>, IMouseDownMethodBind
    {
        public void OnMouseDown() =>
            BindingCallback?.Invoke(transform.position);
    }
}