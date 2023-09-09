using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.UseCases.Inventories.Slots.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class InventorySlotViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly GetInventorySlotItemTypeQuery _getInventorySlotItemTypeQuery;

        public InventorySlotViewModelFactory(
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _getInventorySlotItemTypeQuery = getInventorySlotItemTypeQuery;
        }

        public InventorySlotViewModel Create(int id)
        {
            return new InventorySlotViewModel(new IViewModelComponent[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                },
                id,
                _getInventorySlotItemTypeQuery
            );
        }
    }
}