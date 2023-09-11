using System;
using System.Collections.Generic;
using Presentation.Frameworks.Mvvm.Binders;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.Characters;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Characters.Actions;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Ingredients;
using Sources.Client.Controllers.Ingredients.Actions;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Controllers.Inventories;
using Sources.Client.Controllers.Inventories.Actions;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Infrastructure.Data.Providers;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients.Components;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Presentation.Views;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.GameUpdate;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.Infrastructure.ViewProviders;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Characters.Queries;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;
using Sources.Client.UseCases.Common.Components.Destinations.Commands;
using Sources.Client.UseCases.Common.Components.LookDirection.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using Sources.Client.UseCases.Ingredients.Queries;
using Sources.Client.UseCases.Inventories.Commands;
using Sources.Client.UseCases.Inventories.Listeners;
using Sources.Client.UseCases.Inventories.Queries;
using Sources.Client.UseCases.Inventories.Slots.Queries;
using UnityEngine;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private CharacterMovementService _characterMovementService;
        private CameraFollowService _cameraFollowService;
        private SignalBus _signalBus;
        private SpawnService<Chanterelle> _mushroomSpawnService;
        private SpawnService<ToxicFrog> _frogSpawnService;
        private CurrentPlayerService _currentPlayerService;
        private GetPositionQuery _getPositionQuery;
        private GetSpeedQuery _getSpeedQuery;

        private GameUpdateService _gameUpdateService = new GameUpdateService();
        private EntityRepository _entityRepository;

        private void Awake()
        {
            Environment environment = new EnvironmentDataProvider().Load();
            Camera mainCamera = Camera.main;

            Binder binder = new Binder();
            PrefabFactory prefabFactory = new PrefabFactory();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);
            ViewProvider viewProvider = new ViewProvider();

            _entityRepository = new EntityRepository();

            AbstractIngredientFactory ingredientFactory = new AbstractIngredientFactory();

            _currentPlayerService = new CurrentPlayerService();
            IdGenerator idGenerator = new IdGenerator(10);
            _cameraFollowService = new CameraFollowService(mainCamera.transform.parent);

            SignalHandler signalHandler = new SignalHandler();
            _signalBus = new SignalBus(signalHandler);

            IIngredientType[] ingredientTypes = new IIngredientType[]
            {
                new ToxicFrog(),
                new Chanterelle()
            };

            #region ViewFactories

            IngredientViewFactory ingredientViewFactory = new IngredientViewFactory(prefabFactory);

            InventorySlotViewFactory inventorySlotViewFactory =
                new InventorySlotViewFactory(bindableViewFactory, ingredientViewFactory, ingredientTypes);

            InventoryViewFactory inventoryViewFactory =
                new InventoryViewFactory(bindableViewFactory);

            #endregion

            #region UseCases

            PeasantFactory peasantFactory = new PeasantFactory();

            CreateCurrentCharacterQuery createCurrentCharacterQuery = new CreateCurrentCharacterQuery(
                _entityRepository,
                peasantFactory,
                idGenerator
            );

            MovePositionCommand movePositionCommand = new MovePositionCommand(_entityRepository);
            SetLookDirectionCommand setLookDirectionCommand = new SetLookDirectionCommand(_entityRepository);
            SetSpeedCommand setSpeedCommand = new SetSpeedCommand(_entityRepository);

            _getPositionQuery = new GetPositionQuery(_entityRepository);
            _getSpeedQuery = new GetSpeedQuery(_entityRepository);

            HideCommand hideCommand = new HideCommand(_entityRepository);

            InventoryPushItemCommand inventoryPushItemCommand =
                new InventoryPushItemCommand(_entityRepository);

            InventoryPopItemQuery inventoryPopItemQuery =
                new InventoryPopItemQuery(_entityRepository);

            GetInventoryIdQuery getInventoryIdQuery = new GetInventoryIdQuery(_entityRepository);

            CreateInventoryQuery createInventoryQuery = new CreateInventoryQuery(_entityRepository, idGenerator);
            CreateInventorySlotQuery createInventorySlotQuery =
                new CreateInventorySlotQuery(_entityRepository, idGenerator);

            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery =
                new GetInventorySlotItemTypeQuery(_entityRepository);

            AddInventoryListener addInventoryListener = new AddInventoryListener(_entityRepository);
            RemoveInventoryListener removeInventoryListener = new RemoveInventoryListener(_entityRepository);
            CanPushInventoryQuery canPushInventoryQuery = new CanPushInventoryQuery(_entityRepository);

            CreateIngredientQuery createIngredientQuery =
                new CreateIngredientQuery(_entityRepository, ingredientFactory, idGenerator);

            #endregion

            #region ViewModelFactories

            IngredientInteractorViewModelComponentFactory ingredientInteractorViewModelComponentFactory =
                new IngredientInteractorViewModelComponentFactory(
                    addInventoryListener,
                    removeInventoryListener,
                    canPushInventoryQuery
                );

            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory =
                new VisibilityViewModelComponentFactory(_entityRepository);

            AnimationSpeedViewModelComponentFactory animationSpeedViewModelComponentFactory =
                new AnimationSpeedViewModelComponentFactory(_entityRepository);

            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory =
                new LookDirectionViewModelComponentFactory(_entityRepository);

            CharacterControllerMovementViewModelComponentFactory characterControllerMovementViewModelComponentFactory =
                new CharacterControllerMovementViewModelComponentFactory(_entityRepository);

            PositionViewModelComponentFactory positionViewModelComponentFactory =
                new PositionViewModelComponentFactory(_entityRepository);

            IngredientClickViewModelComponentFactory ingredientClickViewModelComponentFactory =
                new IngredientClickViewModelComponentFactory(_signalBus);

            CharacterViewModelFactory characterViewModelFactory = new CharacterViewModelFactory(
                visibilityViewModelComponentFactory,
                animationSpeedViewModelComponentFactory,
                lookDirectionViewModelComponentFactory,
                characterControllerMovementViewModelComponentFactory,
                ingredientInteractorViewModelComponentFactory
            );

            IngredientViewModelFactoryBase ingredientViewModelFactoryBase = new IngredientViewModelFactoryBase(
                visibilityViewModelComponentFactory,
                positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory
            );

            MoveToDestinationViewModelComponentFactory moveToDestinationViewModelComponentFactory =
                new MoveToDestinationViewModelComponentFactory(_gameUpdateService, _entityRepository);

            ToxicFrogBehaviourTreeViewModelComponentFactory toxicFrogBehaviourTreeViewModelComponentFactory =
                new ToxicFrogBehaviourTreeViewModelComponentFactory(_signalBus, _entityRepository);

            ToxicFrogViewModelFactory toxicFrogViewModelFactory = new ToxicFrogViewModelFactory(
                visibilityViewModelComponentFactory,
                positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory,
                moveToDestinationViewModelComponentFactory,
                toxicFrogBehaviourTreeViewModelComponentFactory,
                lookDirectionViewModelComponentFactory
            );

            IngredientViewModelFactory ingredientViewModelFactory = new IngredientViewModelFactory(
                ingredientViewModelFactoryBase,
                new Dictionary<Type, IIngredientViewModelFactory>()
                {
                    [typeof(ToxicFrog)] = toxicFrogViewModelFactory
                }
            );

            InventoryViewModelFactory inventoryViewModelFactory =
                new InventoryViewModelFactory(visibilityViewModelComponentFactory);

            InventorySlotViewModelFactory inventorySlotViewModelFactory = new InventorySlotViewModelFactory(
                visibilityViewModelComponentFactory,
                getInventorySlotItemTypeQuery
            );

            #endregion

            CreateCharacterSignalAction createCharacterSignalAction =
                new CreateCharacterSignalAction(
                    _signalBus,
                    bindableViewFactory,
                    _currentPlayerService,
                    _cameraFollowService,
                    characterViewModelFactory,
                    createCurrentCharacterQuery
                );

            CharacterMoveSignalAction characterMoveSignalAction =
                new CharacterMoveSignalAction(_currentPlayerService, movePositionCommand);
            CharacterRotateSignalAction characterRotateSignalAction =
                new CharacterRotateSignalAction(_currentPlayerService, setLookDirectionCommand);
            CharacterSpeedSignalAction characterSpeedSignalAction =
                new CharacterSpeedSignalAction(_currentPlayerService, setSpeedCommand);

            CharacterSignalController characterSignalController = new CharacterSignalController
            (
                new ISignalAction[]
                {
                    createCharacterSignalAction,
                    characterMoveSignalAction,
                    characterRotateSignalAction,
                    characterSpeedSignalAction,
                }
            );

            IngredientBindableViewFactory ingredientBindableViewFactory =
                new IngredientBindableViewFactory(bindableViewFactory, environment);

            CreateIngredientSignalAction createIngredientSignalAction = new CreateIngredientSignalAction(
                ingredientBindableViewFactory,
                ingredientViewModelFactory,
                createIngredientQuery
            );

            SetDestinationCommand setDestinationCommand = new SetDestinationCommand(_entityRepository);

            ToxicFrogJumpSignalAction toxicFrogJumpSignalAction =
                new ToxicFrogJumpSignalAction(setDestinationCommand, setSpeedCommand);

            IngredientSignalController ingredientSignalController = new IngredientSignalController(
                new ISignalAction[]
                {
                    createIngredientSignalAction,
                    toxicFrogJumpSignalAction
                });

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
                inventoryViewFactory,
                inventoryViewModelFactory,
                viewProvider,
                createInventoryQuery
            );

            CreateInventorySlotSignalAction createInventorySlotSignalAction = new CreateInventorySlotSignalAction(
                viewProvider,
                inventorySlotViewFactory,
                inventorySlotViewModelFactory,
                createInventorySlotQuery
            );

            InventorySignalController inventorySignalController = new InventorySignalController(
                new ISignalAction[]
                {
                    createInventorySignalAction,
                    createInventorySlotSignalAction,
                    inventoryPushSignalAction,
                    inventoryPopSignalAction
                });

            signalHandler.Register(characterSignalController);
            signalHandler.Register(ingredientSignalController);
            signalHandler.Register(inventorySignalController);

            _mushroomSpawnService = new SpawnService<Chanterelle>(_signalBus);
            _frogSpawnService = new SpawnService<ToxicFrog>(_signalBus);
        }

        private void Start()
        {
            _signalBus.Handle(new CreateCharacterSignal(new Vector3(-20, 0, 10)));
            _characterMovementService =
                new CharacterMovementService(_currentPlayerService, _signalBus, Camera.main, _getPositionQuery,
                    _getSpeedQuery);

            _mushroomSpawnService.Spawn();
            _frogSpawnService.Spawn();
        }

        private void Update()
        {
            _characterMovementService.Update();

            _gameUpdateService.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _signalBus.Handle(new ToxicFrogJumpSignal(24, Vector3.zero, 1));
            }
        }

        private void LateUpdate()
        {
            _cameraFollowService.LateUpdate();
        }
    }
}