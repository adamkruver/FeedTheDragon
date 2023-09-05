namespace DomainInterfaces.Frameworks.Mvvm.Methods
{
    public interface IBindableViewMethod
    {
        void OnBind(object callback);
        void Unbind();
    }
}