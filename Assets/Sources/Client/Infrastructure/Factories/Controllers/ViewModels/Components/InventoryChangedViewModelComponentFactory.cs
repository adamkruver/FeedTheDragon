using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.InventoryComponents.Listeners;
using Sources.Client.UseCases.InventoryComponents.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class InventoryChangedViewModelComponentFactory
    {
        private IEntityRepository _entityRepository;
        private readonly IResourceLoader _resourceLoader;

        public InventoryChangedViewModelComponentFactory(
            IEntityRepository entityRepository,
            IResourceLoader resourceLoader)
        {
            _entityRepository = entityRepository;
            _resourceLoader = resourceLoader;
        }

        public IViewModelComponent Create(int id)
        {
            AddInventoryListener addInventoryListener = new AddInventoryListener(_entityRepository);
            RemoveInventoryListener removeInventoryListener = new RemoveInventoryListener(_entityRepository);
            GetInvenoryItemTypesQuery getInvenoryItemTypesQuery = new GetInvenoryItemTypesQuery(_entityRepository);

            return new InventoryChangedViewModelComponent
            (
                _resourceLoader,
                id,
                addInventoryListener,
                removeInventoryListener,
                getInvenoryItemTypesQuery
            );
        }
    }
}