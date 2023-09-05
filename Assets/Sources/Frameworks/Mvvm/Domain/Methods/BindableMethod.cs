using System.Reflection;
using DomainInterfaces.Frameworks.Mvvm.Methods;

namespace Domain.Frameworks.Mvvm.Methods
{
    public class BindableMethod<T> : IBindableMethod<T>
    {
        private readonly object _target;
        private readonly MethodInfo _methodInfo;

        public BindableMethod(object target, MethodInfo methodInfo)
        {
            _target = target;
            _methodInfo = methodInfo;
        }

        public void Invoke(params object[] args) => 
            _methodInfo?.Invoke(_target, args);
    }
}