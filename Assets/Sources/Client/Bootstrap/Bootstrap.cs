using System;
using Presentation.Frameworks.Mvvm.Binders;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.CameraFollower;
using Sources.Client.Characters;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Characters.Actions;
using Sources.Client.Controllers.Characters.SIgnals;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using UnityEngine;

namespace Sources.Client.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private ISignalBus _signalBus;
        private CharacterMovementService _characterMovementService;
        private CameraFollowService _cameraFollowService;

        private void Awake()
        {
            SignalHandler signalHandler = new SignalHandler();
            _signalBus = new SignalBus(signalHandler);

            Binder binder = new Binder();

            CurrentPlayerService currentPlayerService = new CurrentPlayerService();
            _cameraFollowService = new CameraFollowService(Camera.main.transform.parent);

            PeasantFactory peasantFactory = new PeasantFactory();
            EntityRepository entityRepository = new EntityRepository();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);

            CreateCharacterSignalAction createCharacterSignalAction =
                new CreateCharacterSignalAction(
                    peasantFactory,
                    entityRepository,
                    bindableViewFactory,
                    currentPlayerService,
                    _cameraFollowService
                );

            CharacterMoveSignalAction characterMoveSignalAction = new CharacterMoveSignalAction(entityRepository);

            CharacterSignalController characterSignalController = new CharacterSignalController
            (
                new ISignalAction[] { createCharacterSignalAction, characterMoveSignalAction }
            );

            _characterMovementService = new CharacterMovementService(currentPlayerService, _signalBus, Camera.main);

            signalHandler.Register(characterSignalController);

            //_characterMovement.Init(signalBus);
            _signalBus.Handle(new CreateCharacterSignal());
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