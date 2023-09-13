using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels
{
    public interface IViewModelFactory<TViewModel> where TViewModel : IViewModel
    {
        IViewModel Create(int id);
    }
}