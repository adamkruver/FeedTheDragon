using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Triggers
{
    public class TriggerEnterMethodBind : BindableViewMethod<Component>, ITriggerEnterMethodBind
    {
        private void Awake() =>
            GetComponent<Collider>().isTrigger = true;

        public void OnTriggerEnter(Collider other) =>
            BindingCallback?.Invoke(other);
    }
}