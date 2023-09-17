﻿using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.Inventories.Slots.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class InventorySlotViewModelFactory : IViewModelFactory<InventorySlotViewModel>
    {
        private readonly ISignalBus _signalBus;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly GetInventorySlotItemTypeQuery _getInventorySlotItemTypeQuery;

        public InventorySlotViewModelFactory(
            ISignalBus signalBus,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery
        )
        {
            _signalBus = signalBus;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _getInventorySlotItemTypeQuery = getInventorySlotItemTypeQuery;
        }

        public IViewModel Create(int id)
        {
            return new InventorySlotViewModel(new IViewModelComponent[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                },
                id,
                _signalBus,
                _getInventorySlotItemTypeQuery
            );
        }
    }
}