using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.Gameplays;
using Sources.Client.Controllers.Gameplays.States;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Domain.Gameplays.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Builders.Presentation.Fishes;
using Sources.Client.Infrastructure.Factories.Services.CoroutineRunners;
using Sources.Client.Infrastructure.Factories.Services.Fishings;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.Fishing;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.Infrastructure.Services.Pointers.Handlers;
using Sources.Client.Infrastructure.Services.Pools;
using Sources.Client.Infrastructure.Services.Spawn;
using Sources.Client.InfrastructureInterfaces.Providers;
using Sources.Client.InfrastructureInterfaces.Services.Cameras;
using Sources.Client.Presentation.Cameras.Types;
using Sources.Client.Presentation.Views.Fishing;
using UnityEngine;

namespace Sources.Client.Infrastructure.Builders.Gameplays
{
    public class FishingGameplayStateBuilder : GameplayStateBuilderBase<FishingGameplayPayload>
    {
        private readonly ICameraService _cameraService;
        private readonly ICameraProvider _cameraProvider;
        private readonly CoroutineMonoRunnerFactory _coroutineMonoRunnerFactory;
        private readonly PointerService _pointerService;
        private readonly Environment _environment;
        private readonly Fishing _fishing;
        private readonly IPrefabFactory _prefabFactory;

        public FishingGameplayStateBuilder(
            ICameraService cameraService,
            ICameraProvider cameraProvider,
            IPrefabFactory prefabFactory,
            CoroutineMonoRunnerFactory coroutineMonoRunnerFactory,
            PointerService pointerService,
            Environment environment,
            Fishing fishing
        )
        {
            _cameraService = cameraService;
            _cameraProvider = cameraProvider;
            _coroutineMonoRunnerFactory = coroutineMonoRunnerFactory;
            _pointerService = pointerService;
            _environment = environment;
            _fishing = fishing;
            _prefabFactory = prefabFactory;
        }

        protected override IGameplayState BuildState(IStateMachine<IGameplayPayload> stateMachine,
            FishingGameplayPayload payload)
        {
            Camera camera = _cameraProvider.Get<FishingCamera>();

            CatchFishService catchFishService = new CatchFishService(_coroutineMonoRunnerFactory, camera);
            
            FishingBoundsService fishingBoundsService = new FishingBoundsService(camera, _fishing.UnderWaterRect);
            
            FishingLineService fishingLineService =
                new FishingLineServiceFactory(fishingBoundsService, camera).Create(_fishing.FishingCharacter.FishingLine);

            FishingCursorService fishingCursorService =
                new FishingCursorServiceFactory(camera).Create(_fishing.FishingCanvas.FishingLineCursor);


            FishingPointerHandler fishingPointerHandler =
                new FishingPointerHandler(
                    catchFishService,
                    fishingLineService,
                    fishingCursorService
                );

            FishingUntouchedMoveHandler fishingUntouchedMoveHandler =
                new FishingUntouchedMoveHandler(
                    fishingLineService,
                    fishingCursorService
                );
            

            FishPoolService fishPoolService = new FishPoolService(_prefabFactory, _environment,
                _coroutineMonoRunnerFactory, fishingBoundsService);

            FishViewBuilder fishViewBuilder = new FishViewBuilder(fishPoolService);

            FishSpawnService fishSpawnService =
                new FishSpawnService(fishingBoundsService, _coroutineMonoRunnerFactory, fishViewBuilder);

            return new FishingGameplayState
            (
                _cameraService,
                fishSpawnService,
                fishingPointerHandler,
                fishingUntouchedMoveHandler,
                _pointerService,
                fishingBoundsService,
                fishPoolService,
                fishingLineService,
                fishingCursorService
            );
        }
    }
}