using Sources.Client.Characters;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Enemies;
using Sources.Client.Domain.Enemies.Spiders;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.GameUpdate;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.Client.Presentation.Views.SpawnPoints.Enemies;
using Sources.Client.Presentation.Views.SpawnPoints.Ingredients;
using Sources.Client.Presentation.Views.SpawnPoints.NPCs;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.Scenes.Gameplay
{
    public class GameplayScene : ISceneState
    {
        private readonly ISignalBus _signalBus;
        private readonly ISignalHandlerRegisterer _signalHandler;
        private readonly ISignalController[] _signalControllers;
        private readonly CurrentPlayerService _currentPlayerService;
        private readonly SpawnService<IIngredientType, ChanterelleSpawnPoint> _mushroomSpawnService;
        private readonly SpawnService<IIngredientType, ToxicFrogSpawnPoint> _frogSpawnService;
        private readonly SpawnService<IIngredientType, EyeRootSpawnPoint> _eyeRootSpawnService;
        private readonly SpawnService<IIngredientType, DualTongueSpawnPoint> _dualTongueSpawnService;
        private readonly SpawnService<Spider, SpiderSpawnPoint> _spiderSpawnService;
        private readonly SpawnService<Ogre, OgreSpawnPoint> _ogreSpawnService;
        private CharacterMovementService _characterMovementService;
        private GetPositionQuery _getPositionQuery;
        private GetSpeedQuery _getSpeedQuery;
        private readonly GameUpdateService _gameUpdateService;
        private CameraFollowService _cameraFollowService;

        public GameplayScene
        (
            ISignalBus signalBus,
            ISignalHandlerRegisterer signalHandler,
            ISignalController[] signalControllers, 
            CurrentPlayerService currentPlayerService,
            GetPositionQuery getPositionQuery,
            GetSpeedQuery getSpeedQuery,
            GameUpdateService gameUpdateService,
            CameraFollowService cameraFollowService)
        {
            _signalBus = signalBus;
            _signalHandler = signalHandler;
            _signalControllers = signalControllers;
            _currentPlayerService = currentPlayerService;
            _getPositionQuery = getPositionQuery;
            _getSpeedQuery = getSpeedQuery;
            _gameUpdateService = gameUpdateService;
            _cameraFollowService = cameraFollowService;

            _mushroomSpawnService = new SpawnService<IIngredientType, ChanterelleSpawnPoint>(_signalBus);
            _frogSpawnService = new SpawnService<IIngredientType, ToxicFrogSpawnPoint>(_signalBus);
            _eyeRootSpawnService = new SpawnService<IIngredientType, EyeRootSpawnPoint>(_signalBus);
            _dualTongueSpawnService = new SpawnService<IIngredientType, DualTongueSpawnPoint>(_signalBus);
            _ogreSpawnService = new SpawnService<Ogre, OgreSpawnPoint>(_signalBus);
            _spiderSpawnService = new SpawnService<Spider, SpiderSpawnPoint>(_signalBus);
        }

        public void Enter()
        {
            foreach (ISignalController signalController in _signalControllers)
                _signalHandler.Register(signalController);

            _mushroomSpawnService.Spawn();
            _frogSpawnService.Spawn(); // todo переписать
            _eyeRootSpawnService.Spawn();
            _ogreSpawnService.Spawn();
            _dualTongueSpawnService.Spawn();
            _spiderSpawnService.Spawn();

            _signalBus.Handle(new CreateCharacterSignal(new Vector3(-20, 0, 10)));
            _characterMovementService = new CharacterMovementService
                (_currentPlayerService, _signalBus, Camera.main, _getPositionQuery, _getSpeedQuery);
        }

        public void Update(float deltaTime)
        {
            _characterMovementService.Update();
            _gameUpdateService.Update(Time.deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            _cameraFollowService.LateUpdate();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public void Exit()
        {
            foreach (ISignalController signalController in _signalControllers)
                _signalHandler.Unregister(signalController);
        }
    }
}