using System;
using System.Collections.Generic;
using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Gameplays;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Builders.Gameplays;
using Sources.Client.Infrastructure.Factories.Controllers;
using Sources.Client.Infrastructure.Factories.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Factories.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.Infrastructure.StateMachines;
using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Views.Fishing;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Infrastructure.Factories.StateMachines
{
    public class GameplayStateMachineFactory
    {
        private readonly ICameraFollowService _cameraFollowService;
        private readonly CharacterPointerHandlerFactory _characterPointerHandlerFactory;
        private readonly CharacterMovementServiceFactory _characterMovementServiceFactory;
        private readonly PointerService _pointerService;
        private readonly CoroutineMonoRunnerFactory _coroutineMonoRunnerFactory;
        private readonly Environment _environment;
        private readonly Fishing _fishing;
        private readonly IPrefabFactory _prefabFactory;
        private readonly ICameraService _cameraService;
        private readonly ICameraProvider _cameraProvider;

        public GameplayStateMachineFactory(
            ICameraFollowService cameraFollowService,
            ICameraService cameraService,
            ICameraProvider cameraProvider,
            IPrefabFactory prefabFactory,
            CharacterPointerHandlerFactory characterPointerHandlerFactory,
            CharacterMovementServiceFactory characterMovementServiceFactory,
            PointerService pointerService,
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            Environment environment,
            Fishing fishing
        )
        {
            _cameraFollowService = cameraFollowService;
            _characterPointerHandlerFactory = characterPointerHandlerFactory;
            _characterMovementServiceFactory = characterMovementServiceFactory;
            _pointerService = pointerService;
            _coroutineMonoRunnerFactory = coroutineMonoRunnerFactory;
            _environment = environment;
            _fishing = fishing;
            _prefabFactory = prefabFactory;
            _cameraService = cameraService;
            _cameraProvider = cameraProvider;
        }

        public GameplayStateMachine Create()
        {
            CharacterMovementService characterMovementService = _characterMovementServiceFactory.Create();

            MainGameplayStateBuilder mainGameplayStateBuilder = new MainGameplayStateBuilder
            (
                _cameraFollowService,
                characterMovementService,
                _characterPointerHandlerFactory,
                _pointerService,
                _cameraService
            );

            FishingGameplayStateBuilder fishingGameplayStateBuilder = new FishingGameplayStateBuilder
            (
                _cameraService,
                _cameraProvider,
                _prefabFactory,
                _coroutineMonoRunnerFactory,
                _pointerService,
                _environment,
                _fishing
            );

            return new GameplayStateMachine
            (
                new Dictionary<Type, Func<IStateMachine<IGameplayPayload>, IGameplayPayload, IGameplayState>>()
                {
                    [typeof(MainGameplayPayload)] = mainGameplayStateBuilder.Build,
                    [typeof(FishingGameplayPayload)] = fishingGameplayStateBuilder.Build,
                }
            );
        }
    }
}