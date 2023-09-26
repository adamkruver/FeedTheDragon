using System;
using System.Collections.Generic;
using Presentation.Frameworks.Mvvm.Binders;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.App;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Frameworks.StateMachines.Payloads;
using Sources.Client.Infrastructure.Builders.Scenes;
using Sources.Client.Infrastructure.Data.Providers;
using Sources.Client.Infrastructure.SignalBuses;
using Sources.Client.Infrastructure.StateMachines;
using UnityEngine;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Infrastructure.Builders.Apps
{
    public class AppCoreBuilder
    {
        public AppCore Build()
        {
            #region Common

            Environment environment = new EnvironmentDataProvider().Load();
            PrefabFactory prefabFactory = new PrefabFactory();
            
            Binder binder = new Binder();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);
            
            SignalHandler signalHandler = new SignalHandler();
            SignalBus signalBus = new SignalBus(signalHandler);
            
            #endregion

            GameplaySceneStateBuilder gameplaySceneStateBuilder =
                new GameplaySceneStateBuilder(signalBus, signalHandler, bindableViewFactory, prefabFactory, environment);
            InitialSceneBuilder initialSceneBuilder = new InitialSceneBuilder();

            Dictionary<Type, Func<IStateMachine<IScenePayload>, IScenePayload, ISceneState>> stateBuilders =
                new Dictionary<Type, Func<IStateMachine<IScenePayload>, IScenePayload, ISceneState>>
                {
                    [typeof(GameplayPayload)] = gameplaySceneStateBuilder.Build,
                    [typeof(InitialPayload)] = initialSceneBuilder.Build,
                };

            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();
            
            SceneStateMachine appStateMachine = new SceneStateMachine(stateBuilders);

            appCore.Init(appStateMachine);

            return appCore;
        }
    }
}