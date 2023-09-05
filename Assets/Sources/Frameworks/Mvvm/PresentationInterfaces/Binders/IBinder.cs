using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;

namespace PresentationInterfaces.Frameworks.Mvvm.Binders
{
    public interface IBinder
    {
        void Bind(IBindableView view, IViewModel viewModel);
        void Unbind(IBindableView view, IViewModel viewModel);
    }
}