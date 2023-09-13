using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Controllers.Inventories.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Inventories
{
    public class InventorySlotObserverViewModelComponentFactory
    {
        private readonly IBindableViewBuilder<InventorySlotViewModel> _slotViewBuilder;
        private readonly GetInventoryIdsQuery _getInventoryIdsQuery;

        public InventorySlotObserverViewModelComponentFactory(
            IBindableViewBuilder<InventorySlotViewModel> slotViewBuilder,
            GetInventoryIdsQuery getInventoryIdsQuery
        )
        {
            _slotViewBuilder = slotViewBuilder;
            _getInventoryIdsQuery = getInventoryIdsQuery;
        }

        public InventorySlotObserverViewModelComponent Create(int id) =>
            new InventorySlotObserverViewModelComponent(
                id,
                _slotViewBuilder,
                _getInventoryIdsQuery
            );
    }
}