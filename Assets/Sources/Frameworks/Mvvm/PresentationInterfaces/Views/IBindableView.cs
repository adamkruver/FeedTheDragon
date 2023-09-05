using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace PresentationInterfaces.Frameworks.Mvvm.Views
{
    public interface IBindableView
    {
        void Bind(IViewModel viewModel);
        void Unbind();
    }
}