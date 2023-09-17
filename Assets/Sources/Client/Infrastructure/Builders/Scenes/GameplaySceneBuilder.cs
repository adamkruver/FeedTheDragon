﻿using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.Scenes.Gameplay;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Frameworks.StateMachines.Payloads;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.SignalControllers;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components;
using Sources.Client.Infrastructure.Factories.Presentation.Views;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.Infrastructure.Services.GameUpdate;
using Sources.Client.Infrastructure.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Handlers;
using Sources.Client.UseCases.Common.Components.ComponentsListenets;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
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

        protected override ISceneState BuildState(IStateMachine<IScenePayload> stateMachine, GameplayPayload payload)
        {
            #region Configs

            IIngredientType[] availableIngredientTypes =
            {
                new ToxicFrog(),
                new Chanterelle(),
                new EyeRoot(),
                new DualTongue(),
            };

            #endregion

            #region Essentials

            EntityRepository entityRepository = new EntityRepository();
            IdGenerator idGenerator = new IdGenerator(4516); // todo: initial value

            #endregion

            #region Services

            GameUpdateService gameUpdateService = new GameUpdateService();
            CameraFollowService cameraFollowService =
                new CameraFollowService(Camera.main.transform.parent); //todo : to camera provider
            CurrentPlayerService currentPlayerService = new CurrentPlayerService();

            #endregion
            
            #region ViewFactories

            IngredientViewFactory ingredientViewFactory = new IngredientViewFactory(_prefabFactory);

            SlotViewFactory slotViewFactory =
                new SlotViewFactory(_bindableViewFactory, ingredientViewFactory, availableIngredientTypes);

            #endregion

            #region ViewModelComponentFactories

            PositionViewModelComponentFactory positionViewModelComponentFactory =
                new PositionViewModelComponentFactory(entityRepository);

            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory =
                new VisibilityViewModelComponentFactory(entityRepository);

            MoveToDestinationViewModelComponentFactory moveToDestinationViewModelComponentFactory =
                new MoveToDestinationViewModelComponentFactory(gameUpdateService, entityRepository);

            #endregion

            #region Quests //Какая то фабрика для фабрики получилась

            QuestSlotViewModelFactory questSlotViewModelFactory =
                new QuestSlotViewModelFactory(visibilityViewModelComponentFactory, entityRepository);

            BindableViewBuilder<QuestSlotViewModel> questSlotViewBuilder =
                new BindableViewBuilder<QuestSlotViewModel>(
                    slotViewFactory,
                    questSlotViewModelFactory,
                    _environment.View["QuestSlot"]
                );

            GetQuestSlotsIdsQuery getQuestSlotsIdsQuery = new GetQuestSlotsIdsQuery(entityRepository);

            QuestSlotObserverViewModelComponentFactory questSlotObserverViewModelComponentFactory =
                new QuestSlotObserverViewModelComponentFactory(getQuestSlotsIdsQuery, questSlotViewBuilder);

            QuestViewModelFactory questViewModelFactory =
                new QuestViewModelFactory(visibilityViewModelComponentFactory,
                    questSlotObserverViewModelComponentFactory);

            BindableViewBuilder<QuestViewModel> questViewBuilder =
                new BindableViewBuilder<QuestViewModel>(
                    _bindableViewFactory,
                    questViewModelFactory,
                    _environment.View["QuestSlot"]
                );
            GetQuestsIdsQuery getQuestsIdsQuery = new GetQuestsIdsQuery(entityRepository);

            AddAfterComponentsChangedListnerCommand addAfterComponentsChangedListnerCommand =
                new AddAfterComponentsChangedListnerCommand(entityRepository);

            RemoveAfterComponentsChangedListnerCommand removeAfterComponentsChangedListnerCommand =
                new RemoveAfterComponentsChangedListnerCommand(entityRepository);

            QuestObserverViewModelComponentFactory questObserverViewModelComponentFactory =
                new QuestObserverViewModelComponentFactory
                (
                    questViewBuilder,
                    addAfterComponentsChangedListnerCommand,
                    removeAfterComponentsChangedListnerCommand,
                    getQuestsIdsQuery
                );

            #endregion

            #region SignalControllerFactories

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
                    questObserverViewModelComponentFactory);

            QuestSignalControllerFactory questSignalControllerFactory =
                new QuestSignalControllerFactory(idGenerator, _signalBus, entityRepository);

            #endregion

            return new GameplayScene(
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