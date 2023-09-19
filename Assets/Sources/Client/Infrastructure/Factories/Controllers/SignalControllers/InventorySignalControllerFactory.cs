using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers;
using Sources.Client.Controllers.Inventories.Actions;
using Sources.Client.Controllers.Inventories.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using Sources.Client.UseCases.Inventories.Commands;
using Sources.Client.UseCases.Inventories.Queries;
using Sources.Client.UseCases.Inventories.Slots.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class InventorySignalControllerFactory
    {
        private IEntityRepository _entityRepository;
        private IIdGenerator _idGenerator;
        private ISignalBus _signalBus;
        private ICurrentPlayerService _currentPlayerService;
        private IBindableViewFactory _bindableViewFactory;
        private Environment _environment;
        private VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private IBindableViewFactory _slotViewFactory;

        public InventorySignalControllerFactory(
            IEntityRepository entityRepository,
            IIdGenerator idGenerator,
            ISignalBus signalBus,
            ICurrentPlayerService currentPlayerService,
            IBindableViewFactory bindableViewFactory,
            Environment environment,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            IBindableViewFactory slotViewFactory)
        {
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
            _signalBus = signalBus;
            _currentPlayerService = currentPlayerService;
            _bindableViewFactory = bindableViewFactory;
            _environment = environment;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _slotViewFactory = slotViewFactory;
        }

        public SignalController Create()
        {
            HideCommand hideCommand = new HideCommand(_entityRepository);
            InventoryPushItemCommand inventoryPushItemCommand = new InventoryPushItemCommand(_entityRepository);
            InventoryPopItemQuery inventoryPopItemQuery = new InventoryPopItemQuery(_entityRepository);
            GetInventoryIdQuery getInventoryIdQuery = new GetInventoryIdQuery(_entityRepository);
            CreateInventoryQuery createInventoryQuery = new CreateInventoryQuery(_entityRepository, _idGenerator);
            DropInventoryItemCommand dropInventoryItemCommand =
                new DropInventoryItemCommand(_entityRepository, _currentPlayerService);

            CreateInventorySlotQuery createInventorySlotQuery =
                new CreateInventorySlotQuery(_entityRepository, _idGenerator);

            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery =
                new GetInventorySlotItemTypeQuery(_entityRepository);

            GetInventoryIdsQuery getInventoryIdsQuery = new GetInventoryIdsQuery(_entityRepository);

            InventorySlotViewModelFactory inventorySlotViewModelFactory = new InventorySlotViewModelFactory(
                _signalBus,
                _visibilityViewModelComponentFactory,
                getInventorySlotItemTypeQuery
            );

            BindableViewBuilder<InventorySlotViewModel> inventorySlotViewBuilder =
                new BindableViewBuilder<InventorySlotViewModel>(
                    _slotViewFactory,
                    inventorySlotViewModelFactory,
                    _environment.View["Inventory"]
                );

            InventorySlotObserverViewModelComponentFactory inventorySlotObserverViewModelComponentFactory =
                new InventorySlotObserverViewModelComponentFactory(inventorySlotViewBuilder, getInventoryIdsQuery);

            InventoryViewModelFactory inventoryViewModelFactory = new InventoryViewModelFactory(
                _visibilityViewModelComponentFactory,
                inventorySlotObserverViewModelComponentFactory
            );

            BindableViewBuilder<InventoryViewModel> inventoryViewBuilder = new BindableViewBuilder<InventoryViewModel>(
                _bindableViewFactory,
                inventoryViewModelFactory,
                _environment.View["Inventory"]
            );

            InventoryPushSignalAction inventoryPushSignalAction = new InventoryPushSignalAction(
                _currentPlayerService,
                inventoryPushItemCommand,
                getInventoryIdQuery,
                hideCommand
            );

            InventoryPopSignalAction inventoryPopSignalAction = new InventoryPopSignalAction(
                _currentPlayerService,
                inventoryPopItemQuery,
                getInventoryIdQuery
            );

            CreateInventorySignalAction createInventorySignalAction = new CreateInventorySignalAction(
                _signalBus,
                inventoryViewBuilder,
                createInventoryQuery
            );

            CreateInventorySlotSignalAction createInventorySlotSignalAction = new CreateInventorySlotSignalAction(
                createInventorySlotQuery
            );

            DropInventoryItemSignalAction dropInventoryItemSignalAction =
                new DropInventoryItemSignalAction(dropInventoryItemCommand);

            return new SignalController(
                new ISignalAction[]
                {
                    createInventorySignalAction,
                    createInventorySlotSignalAction,
                    inventoryPushSignalAction,
                    inventoryPopSignalAction,
                    dropInventoryItemSignalAction,
                });
        }
    }
}