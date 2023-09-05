namespace DomainInterfaces.Frameworks.Mvvm.Properties.Generic
{
    public interface IBindableViewProperty<T> : IBindableViewProperty
    {
        IBindableProperty<T> GetBinding(IBindablePropertyFactory factory);

        object IBindableViewProperty.OnBind(IBindablePropertyFactory factory) => GetBinding(factory);
    }
}