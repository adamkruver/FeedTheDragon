using Presentation.Frameworks.Mvvm.Binders;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.Characters;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Characters.Actions;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Ingredients;
using Sources.Client.Controllers.Ingredients.Actions;
using Sources.Client.Controllers.Inventories;
using Sources.Client.Controllers.Inventories.Actions;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Presentation.Views;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.Infrastructure.ViewProviders;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Characters.Queries;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Commands;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;
using Sources.Client.UseCases.Common.Components.LookDirection.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using Sources.Client.UseCases.Inventories.Commands;
using Sources.Client.UseCases.Inventories.Listeners;
using Sources.Client.UseCases.Inventories.Queries;
using Sources.Client.UseCases.Inventories.Slots.Queries;
using UnityEngine;

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

        private void Awake()
        {
            Camera mainCamera = Camera.main;

            Binder binder = new Binder();
            PrefabFactory prefabFactory = new PrefabFactory();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);
            ViewProvider viewProvider = new ViewProvider();

            EntityRepository entityRepository = new EntityRepository();

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
                entityRepository,
                peasantFactory,
                idGenerator
            );

            MovePositionCommand movePositionCommand = new MovePositionCommand(entityRepository);
            SetLookDirectionCommand setLookDirectionCommand = new SetLookDirectionCommand(entityRepository);
            SetAnimationSpeedCommand setAnimationSpeedCommand = new SetAnimationSpeedCommand(entityRepository);

            _getPositionQuery = new GetPositionQuery(entityRepository);
            _getSpeedQuery = new GetSpeedQuery(entityRepository);

            HideCommand hideCommand = new HideCommand(entityRepository);

            InventoryPushItemCommand inventoryPushItemCommand =
                new InventoryPushItemCommand(entityRepository);

            InventoryPopItemQuery inventoryPopItemQuery =
                new InventoryPopItemQuery(entityRepository);

            GetInventoryIdQuery getInventoryIdQuery = new GetInventoryIdQuery(entityRepository);

            CreateInventoryQuery createInventoryQuery = new CreateInventoryQuery(entityRepository, idGenerator);
            CreateInventorySlotQuery createInventorySlotQuery =
                new CreateInventorySlotQuery(entityRepository, idGenerator);

            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery =
                new GetInventorySlotItemTypeQuery(entityRepository);

            AddInventoryListener addInventoryListener = new AddInventoryListener(entityRepository);
            RemoveInventoryListener removeInventoryListener = new RemoveInventoryListener(entityRepository);
            CanPushInventoryQuery canPushInventoryQuery = new CanPushInventoryQuery(entityRepository);

            #endregion

            #region ViewModelFactories

            IngredientInteractorViewModelComponentFactory ingredientInteractorViewModelComponentFactory =
                new IngredientInteractorViewModelComponentFactory(
                    addInventoryListener,
                    removeInventoryListener,
                    canPushInventoryQuery
                );

            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory =
                new VisibilityViewModelComponentFactory(entityRepository);

            AnimationSpeedViewModelComponentFactory animationSpeedViewModelComponentFactory =
                new AnimationSpeedViewModelComponentFactory(entityRepository);

            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory =
                new LookDirectionViewModelComponentFactory(entityRepository);

            CharacterControllerMovementViewModelComponentFactory characterControllerMovementViewModelComponentFactory =
                new CharacterControllerMovementViewModelComponentFactory(entityRepository);

            PositionViewModelComponentFactory positionViewModelComponentFactory =
                new PositionViewModelComponentFactory(entityRepository);

            IngredientClickViewModelComponentFactory ingredientClickViewModelComponentFactory =
                new IngredientClickViewModelComponentFactory(_signalBus);

            CharacterViewModelFactory characterViewModelFactory = new CharacterViewModelFactory(
                visibilityViewModelComponentFactory,
                animationSpeedViewModelComponentFactory,
                lookDirectionViewModelComponentFactory,
                characterControllerMovementViewModelComponentFactory,
                ingredientInteractorViewModelComponentFactory
            );

            IngredientViewModelFactory ingredientViewModelFactory = new IngredientViewModelFactory(
                visibilityViewModelComponentFactory,
                positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory
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
                    createCurrentCharacterQuery,
                    inventoryViewModelFactory,
                    inventoryViewFactory
                );

            CharacterMoveSignalAction characterMoveSignalAction =
                new CharacterMoveSignalAction(_currentPlayerService, movePositionCommand);
            CharacterRotateSignalAction characterRotateSignalAction =
                new CharacterRotateSignalAction(_currentPlayerService, setLookDirectionCommand);
            CharacterSpeedSignalAction characterSpeedSignalAction =
                new CharacterSpeedSignalAction(_currentPlayerService, setAnimationSpeedCommand);

            CharacterSignalController characterSignalController = new CharacterSignalController(
                new ISignalAction[]
                {
                    createCharacterSignalAction,
                    characterMoveSignalAction,
                    characterRotateSignalAction,
                    characterSpeedSignalAction,
                }
            );

            IngredientFactory ingredientFactory = new IngredientFactory();

            CreateIngredientSignalAction createIngredientSignalAction = new CreateIngredientSignalAction(
                entityRepository,
                ingredientFactory,
                idGenerator,
                bindableViewFactory,
                ingredientViewModelFactory
            );

            IngredientSignalController ingredientSignalController = new IngredientSignalController(
                new ISignalAction[]
                {
                    createIngredientSignalAction
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

            if (Input.GetKeyDown(KeyCode.Space))
                _signalBus.Handle(new PopInventorySignal());
        }

        private void LateUpdate()
        {
            _cameraFollowService.LateUpdate();
        }
    }
}