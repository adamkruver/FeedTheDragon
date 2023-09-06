using System;
using Presentation.Frameworks.Mvvm.Binders;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.Characters;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Characters.Actions;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Ingredients;
using Sources.Client.Controllers.Ingredients.Actions;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Domain.Ingredients;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Characters.Queries;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Commands;
using Sources.Client.UseCases.Common.Components.LookDirections.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;
using UnityEngine;

namespace Sources.Client.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private CharacterMovementService _characterMovementService;
        private CameraFollowService _cameraFollowService;
        private SignalBus _signalBus;
        private SpawnService<Mushroom> _mushroomSpawnService;

        private void Awake()
        {
            Camera mainCamera = Camera.main;

            Binder binder = new Binder();
            CurrentPlayerService currentPlayerService = new CurrentPlayerService();
            IdGenerator idGenerator = new IdGenerator(10);
            _cameraFollowService = new CameraFollowService(mainCamera.transform.parent);

            SignalHandler signalHandler = new SignalHandler();
            _signalBus = new SignalBus(signalHandler);

            PeasantFactory peasantFactory = new PeasantFactory();
            EntityRepository entityRepository = new EntityRepository();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);
            
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

            CharacterViewModelFactory characterViewModelFactory = new CharacterViewModelFactory
            (
                visibilityViewModelComponentFactory,
                animationSpeedViewModelComponentFactory,
                lookDirectionViewModelComponentFactory,
                characterControllerMovementViewModelComponentFactory
            );

            IngredientViewModelFactory ingredientViewModelFactory = new IngredientViewModelFactory
            (
                visibilityViewModelComponentFactory,
                positionViewModelComponentFactory
            );

            #endregion

            #region UseCases

            CreateCurrentCharacterQuery createCurrentCharacterQuery = new CreateCurrentCharacterQuery
            (
                entityRepository,
                peasantFactory,
                idGenerator
            );
            
            MovePositionCommand movePositionCommand = new MovePositionCommand(entityRepository);
            SetLookDirectionCommand setLookDirectionCommand = new SetLookDirectionCommand(entityRepository);
            SetAnimationSpeedCommand setAnimationSpeedCommand = new SetAnimationSpeedCommand(entityRepository);
            
            GetPositionQuery getPositionQuery = new GetPositionQuery(entityRepository);
            GetSpeedQuery getSpeedQuery = new GetSpeedQuery(entityRepository);
            
            #endregion

            CreateCharacterSignalAction createCharacterSignalAction =
                new CreateCharacterSignalAction(
                    peasantFactory,
                    entityRepository,
                    bindableViewFactory,
                    currentPlayerService,
                    _cameraFollowService,
                    idGenerator,
                    characterViewModelFactory,
                    createCurrentCharacterQuery
                );

            CharacterMoveSignalAction characterMoveSignalAction = 
                new CharacterMoveSignalAction(currentPlayerService, movePositionCommand);
            CharacterRotateSignalAction characterRotateSignalAction =
                new CharacterRotateSignalAction(currentPlayerService, setLookDirectionCommand);
            CharacterSpeedSignalAction characterSpeedSignalAction =
                new CharacterSpeedSignalAction(currentPlayerService, setAnimationSpeedCommand);

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

            _characterMovementService = 
                new CharacterMovementService(currentPlayerService, _signalBus, mainCamera, getPositionQuery, getSpeedQuery);

            IngredientFactory ingredientFactory = new IngredientFactory();

            CreateIngredientSignalAction createIngredientSignalAction =
                new CreateIngredientSignalAction
                (
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

            signalHandler.Register(characterSignalController);
            signalHandler.Register(ingredientSignalController);

            _mushroomSpawnService = new SpawnService<Mushroom>(_signalBus);
        }

        private void Start()
        {
            _signalBus.Handle(new CreateCharacterSignal(new Vector3(-20, 0, 10)));

            _mushroomSpawnService.Spawn();
           // _signalBus.Handle(new CreateIngredientSignal(new Mushroom(), Vector3.forward * 5));
        }

        private void Update()
        {
            _characterMovementService.Update();
        }

        private void LateUpdate()
        {
            _cameraFollowService.LateUpdate();
        }
    }
}