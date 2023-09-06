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
using Sources.Client.Domain.Characters;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Domain.Ingredients;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using UnityEngine;

namespace Sources.Client.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private CharacterMovementService _characterMovementService;
        private CameraFollowService _cameraFollowService;

        private void Awake()
        {
            Camera mainCamera = Camera.main;

            Binder binder = new Binder();
            CurrentPlayerService currentPlayerService = new CurrentPlayerService();
            IdGenerator idGenerator = new IdGenerator(10);
            _cameraFollowService = new CameraFollowService(mainCamera.transform.parent);

            SignalHandler signalHandler = new SignalHandler();
            ISignalBus signalBus = new SignalBus(signalHandler);

            PeasantFactory peasantFactory = new PeasantFactory();
            EntityRepository entityRepository = new EntityRepository();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);

            CreateCharacterSignalAction createCharacterSignalAction =
                new CreateCharacterSignalAction(
                    peasantFactory,
                    entityRepository,
                    bindableViewFactory,
                    currentPlayerService,
                    _cameraFollowService,
                    idGenerator
                );

            CharacterMoveSignalAction characterMoveSignalAction = new CharacterMoveSignalAction(currentPlayerService);
            CharacterRotateSignalAction characterRotateSignalAction = new CharacterRotateSignalAction(currentPlayerService);
            CharacterSpeedSignalAction characterSpeedSignalAction = new CharacterSpeedSignalAction(currentPlayerService);

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

            _characterMovementService = new CharacterMovementService(currentPlayerService, signalBus, mainCamera);

            IngredientFactory ingredientFactory = new IngredientFactory();
            
            CreateIngredientSignalAction createIngredientSignalAction =
                new CreateIngredientSignalAction
                (
                    entityRepository,
                    ingredientFactory,
                    idGenerator,
                    bindableViewFactory
                );

            IngredientSignalController ingredientSignalController = new IngredientSignalController(
                new ISignalAction[]
                {
                    createIngredientSignalAction
                });

            signalHandler.Register(characterSignalController);
            signalHandler.Register(ingredientSignalController);

            signalBus.Handle(new CreateCharacterSignal(Vector3.zero));
            signalBus.Handle(new CreateIngredientSignal(new Mushroom(), Vector3.forward * 5));
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