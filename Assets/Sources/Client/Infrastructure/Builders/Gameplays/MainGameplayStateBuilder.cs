using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Gameplays;
using Sources.Client.Controllers.Gameplays.States;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Factories.Controllers;
using Sources.Client.Infrastructure.Factories.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.Infrastructure.Services.Pointers.Handlers;
using Sources.Client.InfrastructureInterfaces.Pointers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using UnityEngine;

namespace Sources.Client.Infrastructure.Builders.Gameplays
{
    public class MainGameplayStateBuilder : GameplayStateBuilderBase<MainGameplayPayload>
    {
        private readonly ICameraFollowService _cameraFollowService;
        private readonly CharacterMovementService _characterMovementService;
        private readonly CharacterPointerHandlerFactory _characterPointerHandlerFactory;
        private readonly PointerService _pointerService;
        private readonly ICameraService _cameraService;

        public MainGameplayStateBuilder(
            ICameraFollowService cameraFollowService,
            CharacterMovementService characterMovementService,
            CharacterPointerHandlerFactory characterPointerHandlerFactory,
            PointerService pointerService,
            ICameraService cameraService
        )
        {
            _cameraFollowService = cameraFollowService;
            _characterMovementService = characterMovementService;
            _characterPointerHandlerFactory = characterPointerHandlerFactory;
            _pointerService = pointerService;
            _cameraService = cameraService;
        }

        protected override IGameplayState BuildState(IStateMachine<IGameplayPayload> stateMachine,
            MainGameplayPayload payload)
        {
            IPointerHandler pointerHandler = _characterPointerHandlerFactory.Create(_characterMovementService);

            return new MainGameplayState
            (
                _cameraFollowService,
                pointerHandler,
                _characterMovementService,
                _pointerService,
                _cameraService
            );
        }
    }
}