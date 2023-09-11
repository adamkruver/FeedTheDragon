using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Sources.Client.InfrastructureInterfaces.Factories.Controllers
{
    public interface IIngredientViewModelFactory
    {
        public IViewModel Create(int id);
    }
}