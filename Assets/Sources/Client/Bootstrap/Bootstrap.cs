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
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Domain.Ingredients;
using Sources.Client.Infrastructure.Providers;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Characters.Queries;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Commands;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;
using Sources.Client.UseCases.Common.Components.LookDirections.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Visibilities.Commands;
using Sources.Client.UseCases.InventoryComponents.Commands;
using Sources.Client.UseCases.InventoryComponents.Queries;
using UnityEngine;

namespace Sources.Client.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private CharacterMovementService _characterMovementService;
        private CameraFollowService _cameraFollowService;
        private SignalBus _signalBus;
        private SpawnService<Mushroom> _mushroomSpawnService;
        private SpawnService<ToxicFrog> _frogSpawnService;

        private void Awake()
        {
            Camera mainCamera = Camera.main;

            ResourceLoader resourceLoader = new ResourceLoader();

            Binder binder = new Binder();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);

            EntityRepository entityRepository = new EntityRepository();

            CurrentPlayerService currentPlayerService = new CurrentPlayerService();
            IdGenerator idGenerator = new IdGenerator(10);
            _cameraFollowService = new CameraFollowService(mainCamera.transform.parent);

            SignalHandler signalHandler = new SignalHandler();
            _signalBus = new SignalBus(signalHandler);

            #region ViewModelFactories

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
                characterControllerMovementViewModelComponentFactory
            );

            IngredientViewModelFactory ingredientViewModelFactory = new IngredientViewModelFactory(
                visibilityViewModelComponentFactory,
                positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory
            );

            InventoryViewModelFactory inventoryViewModelFactory =
                new InventoryViewModelFactory(entityRepository, visibilityViewModelComponentFactory, resourceLoader);

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

            GetPositionQuery getPositionQuery = new GetPositionQuery(entityRepository);
            GetSpeedQuery getSpeedQuery = new GetSpeedQuery(entityRepository);

            HideCommand hideCommand = new HideCommand(entityRepository);
            CanPushInventoryQuery canPushInventoryQuery = new CanPushInventoryQuery(entityRepository);

            PushIngredientToInventoryCommand pushIngredientToInventoryCommand =
                new PushIngredientToInventoryCommand(entityRepository);

            TryPopIngredientFromInventoryQuery tryPopIngredientFromInventoryQuery =
                new TryPopIngredientFromInventoryQuery(entityRepository);

            #endregion

            CreateCharacterSignalAction createCharacterSignalAction =
                new CreateCharacterSignalAction(
                    bindableViewFactory,
                    currentPlayerService,
                    _cameraFollowService,
                    characterViewModelFactory,
                    createCurrentCharacterQuery,
                    inventoryViewModelFactory
                );

            CharacterMoveSignalAction characterMoveSignalAction =
                new CharacterMoveSignalAction(currentPlayerService, movePositionCommand);
            CharacterRotateSignalAction characterRotateSignalAction =
                new CharacterRotateSignalAction(currentPlayerService, setLookDirectionCommand);
            CharacterSpeedSignalAction characterSpeedSignalAction =
                new CharacterSpeedSignalAction(currentPlayerService, setAnimationSpeedCommand);

            CharacterSignalController characterSignalController = new CharacterSignalController(
                new ISignalAction[]
                {
                    createCharacterSignalAction,
                    characterMoveSignalAction,
                    characterRotateSignalAction,
                    characterSpeedSignalAction,
                }
            );

            _characterMovementService =
                new CharacterMovementService(currentPlayerService, _signalBus, mainCamera, getPositionQuery,
                    getSpeedQuery);

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
                entityRepository,
                currentPlayerService,
                canPushInventoryQuery,
                pushIngredientToInventoryCommand,
                hideCommand
            );

            InventoryPopSignalAction inventoryPopSignalAction = new InventoryPopSignalAction(
                currentPlayerService,
                entityRepository,
                tryPopIngredientFromInventoryQuery
            );

            InventorySignalController inventorySignalController = new InventorySignalController(
                new ISignalAction[]
                {
                    inventoryPushSignalAction,
                    inventoryPopSignalAction
                });

            signalHandler.Register(characterSignalController);
            signalHandler.Register(ingredientSignalController);
            signalHandler.Register(inventorySignalController);

            _mushroomSpawnService = new SpawnService<Mushroom>(_signalBus);
            _frogSpawnService = new SpawnService<ToxicFrog>(_signalBus);
        }

        private void Start()
        {
            _signalBus.Handle(new CreateCharacterSignal(new Vector3(-20, 0, 10)));

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