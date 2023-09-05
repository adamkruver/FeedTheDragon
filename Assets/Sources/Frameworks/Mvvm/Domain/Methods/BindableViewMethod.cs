using DomainInterfaces.Frameworks.Mvvm.Methods;
using DomainInterfaces.Frameworks.Mvvm.Methods.Generic;
using UnityEngine;

namespace Domain.Frameworks.Mvvm.Methods
{
    public abstract class BindableViewMethod<T> : MonoBehaviour, IBindableViewMethod<T>
    {
        protected IBindableMethod<T> BindingCallback { get; private set; }

        public void BindCallback(IBindableMethod<T> callback) =>
            BindingCallback = callback;

        public void Unbind() =>
            BindingCallback = new BindableMethod<T>(null, null);
    }
}