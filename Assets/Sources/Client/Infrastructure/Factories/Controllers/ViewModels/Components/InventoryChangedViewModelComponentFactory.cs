using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.InventoryComponents.Listeners;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class InventoryChangedViewModelComponentFactory
    {
        private IEntityRepository _entityRepository;

        public InventoryChangedViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IViewModelComponent Create(int id)
        {
            AddInventoryListener addInventoryListener = new AddInventoryListener(_entityRepository);
            RemoveInventoryListener removeInventoryListener = new RemoveInventoryListener(_entityRepository);
            
            return new InventoryChangedViewModelComponent(
                id,
                addInventoryListener,
                removeInventoryListener
            );
        }
    }
}