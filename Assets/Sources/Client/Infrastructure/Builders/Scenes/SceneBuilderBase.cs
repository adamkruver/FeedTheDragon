using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.InfrastructureInterfaces.Builders.Scenes;

namespace Sources.Client.Infrastructure.Builders.Scenes
{
    public abstract class SceneBuilderBase<TPayload> where TPayload : IScenePayload
    {
        public ISceneState Build(IStateMachine<IScenePayload> stateMachine, IScenePayload payload) =>
            BuildState(stateMachine, (TPayload)payload);

        protected abstract ISceneState BuildState(IStateMachine<IScenePayload> stateMachine, TPayload payload); 
    }
}