using System;
using System.Collections.Generic;
using Presentation.Frameworks.Mvvm.Binders;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.App;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.AppStates.Payloads;
using Sources.Client.Frameworks.StateMachines.Payloads;
using Sources.Client.Infrastructure.Builders.Scenes;
using Sources.Client.Infrastructure.Data.Providers;
using Sources.Client.Infrastructure.SignalBus;
using Sources.Client.Infrastructure.StateMachines;
using UnityEngine;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Infrastructure.Builders.Apps
{
    public class AppCoreBuilder
    {
        public AppCore Build()
        {
            Environment environment = new EnvironmentDataProvider().Load();
            Binder binder = new Binder();
            SignalHandler signalHandler = new SignalHandler();
            BindableViewFactory bindableViewFactory = new BindableViewFactory(binder);
            PrefabFactory prefabFactory = new PrefabFactory();
            SignalBus.SignalBus signalBus = new SignalBus.SignalBus(signalHandler);

            GameplaySceneBuilder gameplaySceneBuilder =
                new GameplaySceneBuilder(signalBus, signalHandler, bindableViewFactory, prefabFactory, environment);

            Dictionary<Type, Func<IScenePayload, ISceneState>> stateBuilders =
                new Dictionary<Type, Func<IScenePayload, ISceneState>>
                {
                    [typeof(GameplayPayload)] = gameplaySceneBuilder.Build,
                };

            SceneStateMachine appStateMachine = new SceneStateMachine(stateBuilders);

            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();

            appCore.Init(appStateMachine);

            return appCore;
        }
    }
}