using System;
using System.Collections.Generic;
using Sources.Client.Controllers.Scenes.Signals;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Client.Controllers.Scenes.Actions
{
    public class ChangeSceneSignalAction : ISignalAction<ChangeSceneSignal>
    {
        private readonly IStateMachine<IScenePayload> _sceneStateMachine;

        private Dictionary<Type, Action<IStateMachine<IScenePayload>, IScenePayload>> _sceneLoaders;

        public ChangeSceneSignalAction(IStateMachine<IScenePayload> sceneStateMachine)
        {
            _sceneStateMachine = sceneStateMachine;

            _sceneLoaders = new Dictionary<Type, Action<IStateMachine<IScenePayload>, IScenePayload>>()
            {
                [typeof(InitialPayload)] = (stateMachine, payload) => stateMachine.ChangeState((InitialPayload)payload),
                [typeof(GameplayPayload)] =
                    (stateMachine, payload) => stateMachine.ChangeState((GameplayPayload)payload),
            };
        }

        public void Handle(ChangeSceneSignal signal)
        {
            Type sceneType = signal.ScenePayload.GetType();
            if (_sceneLoaders.ContainsKey(sceneType) == false)
                throw new NullReferenceException();
            
            _sceneLoaders[sceneType].Invoke(_sceneStateMachine, signal.ScenePayload);
        }
    }
}