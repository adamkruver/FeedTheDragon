using System;

namespace DomainInterfaces.Frameworks.Mvvm.Properties
{
    public interface IBindableProperty<T> : IDisposable
    {
        public event Action Changed;

        public T Value { get; set; }
    }
}