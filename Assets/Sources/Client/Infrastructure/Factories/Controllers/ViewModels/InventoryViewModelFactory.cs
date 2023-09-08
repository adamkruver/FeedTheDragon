using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class InventoryViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;

        public InventoryViewModelFactory(
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new InventoryViewModel(
                new[]
                {
                    _visibilityViewModelComponentFactory.Create(id)
                }
            );
        }
    }
}