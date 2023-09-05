using DomainInterfaces.Frameworks.Mvvm.Properties;
using DomainInterfaces.Frameworks.Mvvm.Properties.Generic;
using UnityEngine;

namespace Domain.Frameworks.Mvvm.Properties
{
    public abstract class BindableViewProperty<T> : MonoBehaviour, IBindableViewProperty<T>
    {
        private IBindableProperty<T> _property;

        public IBindableProperty<T> GetBinding(IBindablePropertyFactory factory) => 
            factory.Create<T>(this, nameof(BindableProperty));

        public abstract T BindableProperty { get; set; }
    }
}