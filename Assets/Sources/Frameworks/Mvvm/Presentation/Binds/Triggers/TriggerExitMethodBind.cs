using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Triggers
{
    public class TriggerExitMethodBind : BindableViewMethod<Component>, ITriggerExitMethodBind
    {
        private void Awake() =>
            GetComponent<Collider>().isTrigger = true;

        public void OnTriggerExit(Collider other) =>
            BindingCallback.Invoke(other);
    }}