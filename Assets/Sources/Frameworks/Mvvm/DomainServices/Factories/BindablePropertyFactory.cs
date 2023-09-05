using System.Reflection;
using Domain.Frameworks.Mvvm.Properties;
using DomainInterfaces.Frameworks.Mvvm.Properties;

namespace DomainServices.Frameworks.Mvvm.Factories
{
    public class BindablePropertyFactory : IBindablePropertyFactory
    {
        private static readonly BindingFlags s_bindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public IBindableProperty<T> Create<T>(object target, string propertyName) =>
            new BindableProperty<T>(target, target.GetType().GetProperty(propertyName, s_bindingFlags));
    }
}