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

        public ChangeSceneSignalAction(IStateMachine<IScenePayload> sceneStateMachine)
        {
            _sceneStateMachine = sceneStateMachine;
        }

        public void Handle(ChangeSceneSignal signal)
        {
            dynamic payload = signal.ScenePayload; // TODO: CreateScenesDictionary;
            _sceneStateMachine.ChangeState(payload);
        }
    }
}