namespace DomainInterfaces.Frameworks.Mvvm.Properties
{
    public interface IBindablePropertyFactory
    {
        IBindableProperty<T> Create<T>(object target, string propertyName);
    }
}