using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Frameworks.StateMachines.Payloads;
using Sources.Client.Infrastructure.Factories.Controllers.SignalControllers;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.Infrastructure.Factories.Presentation.Views;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.GameUpdate;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;
using UnityEngine;

namespace Sources.Client.Infrastructure.Builders.Scenes
{
    public class GameplaySceneBuilder : SceneBuilderBase<GameplayPayload>
    {
        private readonly ISignalBus _signalBus;
        private readonly ISignalHandlerRegisterer _signalHandlerRegisterer;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly IPrefabFactory _prefabFactory;
        private readonly Environment _environment;

        public GameplaySceneBuilder(
            ISignalBus signalBus,
            ISignalHandlerRegisterer signalHandlerRegisterer,
            IBindableViewFactory bindableViewFactory,
            IPrefabFactory prefabFactory,
            Environment environment)
        {
            _signalBus = signalBus;
            _signalHandlerRegisterer = signalHandlerRegisterer;
            _bindableViewFactory = bindableViewFactory;
            _prefabFactory = prefabFactory;
            _environment = environment;
        }

        protected override ISceneState BuildState(GameplayPayload payload)
        {
            IIngredientType[] availableIngredientTypes =
            {
                new ToxicFrog(),
                new Chanterelle(),
                new EyeRoot(),
                new DualTongue(),
            };

            EntityRepository entityRepository = new EntityRepository();
            IdGenerator idGenerator = new IdGenerator(4516); // todo: initial value
            GameUpdateService gameUpdateService = new GameUpdateService();
            CameraFollowService cameraFollowService =
                new CameraFollowService(Camera.main.transform.parent); //todo : to camera provider
            CurrentPlayerService currentPlayerService = new CurrentPlayerService();

            PositionViewModelComponentFactory positionViewModelComponentFactory =
                new PositionViewModelComponentFactory(entityRepository);

            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory =
                new VisibilityViewModelComponentFactory(entityRepository);

            MoveToDestinationViewModelComponentFactory moveToDestinationViewModelComponentFactory =
                new MoveToDestinationViewModelComponentFactory(gameUpdateService, entityRepository);

            IngredientViewFactory ingredientViewFactory = new IngredientViewFactory(_prefabFactory);

            SlotViewFactory slotViewFactory =
                new SlotViewFactory(_bindableViewFactory, ingredientViewFactory, availableIngredientTypes);

            CharacterSignalControllerFactory characterSignalControllerFactory =
                new CharacterSignalControllerFactory(currentPlayerService, _signalBus, cameraFollowService,
                    _bindableViewFactory, _environment, entityRepository, idGenerator);

            IngredientSignalControllerFactory ingredientSignalControllerFactory =
                new IngredientSignalControllerFactory(entityRepository, _environment, _bindableViewFactory,
                    positionViewModelComponentFactory, visibilityViewModelComponentFactory, _signalBus,
                    moveToDestinationViewModelComponentFactory, idGenerator);

            InventorySignalControllerFactory inventorySignalControllerFactory =
                new InventorySignalControllerFactory(entityRepository, idGenerator, _signalBus, currentPlayerService,
                    _bindableViewFactory, _environment, visibilityViewModelComponentFactory, slotViewFactory);

            OgreSignalControllerFactory ogreSignalControllerFactory =
                new OgreSignalControllerFactory(idGenerator, _signalBus, entityRepository, _environment,
                    _bindableViewFactory, positionViewModelComponentFactory, visibilityViewModelComponentFactory,
                    slotViewFactory);

            QuestSignalControllerFactory questSignalControllerFactory =
                new QuestSignalControllerFactory(idGenerator, _signalBus, entityRepository);

            return new GameplayState(
                _signalBus,
                _signalHandlerRegisterer,
                new ISignalController[]
                {
                    characterSignalControllerFactory.Create(),
                    ingredientSignalControllerFactory.Create(),
                    inventorySignalControllerFactory.Create(),
                    ogreSignalControllerFactory.Create(),
                    questSignalControllerFactory.Create(availableIngredientTypes),
                },
                currentPlayerService,
                new GetPositionQuery(entityRepository),
                new GetSpeedQuery(entityRepository),
                gameUpdateService,
                cameraFollowService);
        }
    }
}