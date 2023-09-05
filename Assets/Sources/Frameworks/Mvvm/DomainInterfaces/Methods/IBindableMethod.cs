namespace DomainInterfaces.Frameworks.Mvvm.Methods
{
    public interface IBindableMethod<T>
    {
        void Invoke(params object[] args);
    }
}