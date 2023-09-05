using System.Reflection;

namespace DomainInterfaces.Frameworks.Mvvm.Methods
{
    public interface IBindableMethodFactory
    {
        object Create(object viewModel, MethodInfo methodInfo);
    }
}