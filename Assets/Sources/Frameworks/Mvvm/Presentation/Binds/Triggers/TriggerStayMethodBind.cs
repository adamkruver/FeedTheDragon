using Domain.Frameworks.Mvvm.Methods;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Triggers;
using UnityEngine;

namespace Presentation.Frameworks.Mvvm.Binds.Triggers
{
    public class TriggerStayMethodBind : BindableViewMethod<Component>, ITriggerStayMethodBind
    {
        private void Awake() =>
            GetComponent<Collider>().isTrigger = true;

        public void OnTriggerStay(Collider other) =>
            BindingCallback.Invoke(other);
    }
}