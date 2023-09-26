using System;
using System.Collections.Generic;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Gameplays;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Builders.Gameplays;
using Sources.Client.Infrastructure.Factories.Controllers;
using Sources.Client.Infrastructure.Factories.Presentation.Cameras;
using Sources.Client.Infrastructure.Factories.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.Infrastructure.StateMachines;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;

namespace Sources.Client.Infrastructure.Factories.StateMachines
{
    public class GameplayStateMachineFactory
    {
        private readonly ICameraFollowService _cameraFollowService;
        private readonly CharacterPointerHandlerFactory _characterPointerHandlerFactory;
        private readonly CharacterMovementServiceFactory _characterMovementServiceFactory;
        private readonly PointerService _pointerService;
        private readonly ICameraService _cameraService;

        public GameplayStateMachineFactory(
            ICameraFollowService cameraFollowService,
            CharacterPointerHandlerFactory characterPointerHandlerFactory,
            CharacterMovementServiceFactory characterMovementServiceFactory,
            PointerService pointerService,
            ICameraService cameraService
        )
        {
            _cameraFollowService = cameraFollowService;
            _characterPointerHandlerFactory = characterPointerHandlerFactory;
            _characterMovementServiceFactory = characterMovementServiceFactory;
            _pointerService = pointerService;
            _cameraService = cameraService;
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
            
            FishingGameplayStateBuilder fishingGameplayStateBuilder = new FishingGameplayStateBuilder(_cameraService);

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