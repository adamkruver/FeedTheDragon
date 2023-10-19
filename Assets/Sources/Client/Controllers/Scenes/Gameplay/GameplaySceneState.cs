using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Enemies.Types;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs.Bears;
using Sources.Client.Domain.NPCs.Dragons;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Infrastructure.Factories.StateMachines;
using Sources.Client.Infrastructure.Services.AudioPlayers;
using Sources.Client.Infrastructure.Services.GameUpdate;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.Infrastructure.StateMachines;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.Client.Presentation.Views.SpawnPoints.Enemies;
using Sources.Client.Presentation.Views.SpawnPoints.Ingredients;
using Sources.Client.Presentation.Views.SpawnPoints.NPCs;
using UnityEngine;

namespace Sources.Client.Controllers.Scenes.Gameplay
{
    public class GameplaySceneState : ISceneState
    {
        private readonly ISignalBus _signalBus;
        private readonly ISignalHandlerRegisterer _signalHandler;
        private readonly ISignalController[] _signalControllers;
        private readonly AudioPlayerService _audioPlayerService;

        private readonly SpawnService<IIngredientType, ChanterelleSpawnPoint> _mushroomSpawnService;
        private readonly SpawnService<IIngredientType, ToxicFrogSpawnPoint> _frogSpawnService;
        private readonly SpawnService<IIngredientType, EyeRootSpawnPoint> _eyeRootSpawnService;
        private readonly SpawnService<IIngredientType, DualTongueSpawnPoint> _dualTongueSpawnService;
        private readonly SpawnService<Spider, SpiderSpawnPoint> _spiderSpawnService;
        private readonly SpawnService<Bear, BearSpawnPoint> _bearSpawnService;
        private readonly SpawnService<Ogre, OgreSpawnPoint> _ogreSpawnService;
        private readonly SpawnService<Dragon, DragonSpawnPoint> _dragonSpawnService;

        private readonly GameUpdateService _gameUpdateService;
        private readonly GameplayStateMachineFactory _gameplayStateMachineFactory;
        private CharacterMovementService _characterMovementService;

        private GameplayStateMachine _gameplayStateMachine;

        public GameplaySceneState(
            ISignalBus signalBus,
            ISignalHandlerRegisterer signalHandler,
            ISignalController[] signalControllers,
            AudioPlayerService audioPlayerService,
            GameUpdateService gameUpdateService,
            GameplayStateMachineFactory gameplayStateMachineFactory)
        {
            _signalBus = signalBus;
            _signalHandler = signalHandler;
            _signalControllers = signalControllers;
            _audioPlayerService = audioPlayerService;
            _gameUpdateService = gameUpdateService;
            _gameplayStateMachineFactory = gameplayStateMachineFactory;

            _mushroomSpawnService = new SpawnService<IIngredientType, ChanterelleSpawnPoint>(_signalBus);
            _frogSpawnService = new SpawnService<IIngredientType, ToxicFrogSpawnPoint>(_signalBus);
            _eyeRootSpawnService = new SpawnService<IIngredientType, EyeRootSpawnPoint>(_signalBus);
            _dualTongueSpawnService = new SpawnService<IIngredientType, DualTongueSpawnPoint>(_signalBus);
            _ogreSpawnService = new SpawnService<Ogre, OgreSpawnPoint>(_signalBus);
            _dragonSpawnService = new SpawnService<Dragon, DragonSpawnPoint>(_signalBus);
            _spiderSpawnService = new SpawnService<Spider, SpiderSpawnPoint>(_signalBus);
            _bearSpawnService = new SpawnService<Bear, BearSpawnPoint>(_signalBus);
        }

        public void Enter()
        {
            foreach (ISignalController signalController in _signalControllers)
                _signalHandler.Register(signalController);

            _mushroomSpawnService.Spawn();
            _frogSpawnService.Spawn(); // todo переписать
            _eyeRootSpawnService.Spawn();
            _ogreSpawnService.Spawn();
            _dragonSpawnService.Spawn();
            _dualTongueSpawnService.Spawn();
            _spiderSpawnService.Spawn();
            _bearSpawnService.Spawn();

            _signalBus.Handle(new CreateCharacterSignal(new Vector3(-20, 0, 10)));

            _gameplayStateMachine = _gameplayStateMachineFactory.Create();
            _gameplayStateMachine.Run();
            _audioPlayerService.Enable();
        }

        public void Update(float deltaTime)
        {
            _gameUpdateService.Update(Time.deltaTime);
            _gameplayStateMachine.Update(deltaTime);

            if (Input.GetKeyDown(KeyCode.M))
            {
                _gameplayStateMachine.ChangeState(new MainGameplayPayload());
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _gameplayStateMachine.ChangeState(new FishingGameplayPayload());
            }
        }

        public void LateUpdate(float deltaTime)
        {
            _gameplayStateMachine.LateUpdate(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            _gameplayStateMachine.FixedUpdate(fixedDeltaTime);
        }

        public void Exit()
        {
            _audioPlayerService.Disable();
            
            foreach (ISignalController signalController in _signalControllers)
                _signalHandler.Unregister(signalController);

            _gameplayStateMachine.Stop();
        }
    }
}