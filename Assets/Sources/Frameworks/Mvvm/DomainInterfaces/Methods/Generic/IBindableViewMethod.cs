namespace DomainInterfaces.Frameworks.Mvvm.Methods.Generic
{
    public interface IBindableViewMethod<T> : IBindableViewMethod
    {
        void BindCallback(IBindableMethod<T> callback);
        void IBindableViewMethod.OnBind(object callback) => BindCallback((IBindableMethod<T>)callback);
    }
}