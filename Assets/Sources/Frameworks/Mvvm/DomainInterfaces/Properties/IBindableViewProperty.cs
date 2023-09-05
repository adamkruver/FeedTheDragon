namespace DomainInterfaces.Frameworks.Mvvm.Properties
{
    public interface IBindableViewProperty
    {
        object OnBind(IBindablePropertyFactory factory);
    }
}