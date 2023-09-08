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
        private readonly InventoryChangedViewModelComponentFactory _inventoryChangedViewModelComponentFactory;

        public InventoryViewModelFactory(
            IEntityRepository entityRepository,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            IResourceLoader resourceLoader
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _inventoryChangedViewModelComponentFactory =
                new InventoryChangedViewModelComponentFactory(entityRepository, resourceLoader);
        }

        public IViewModel Create(int id)
        {
            return new InventoryViewModel(
                new[]
                {
                    _inventoryChangedViewModelComponentFactory.Create(id),
                    _visibilityViewModelComponentFactory.Create(id)
                }
            );
        }
    }
}