using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.AppStates.Payloads;
using Sources.Client.InfrastructureInterfaces.Builders.Scenes;

namespace Sources.Client.Infrastructure.Builders.Scenes
{
    public abstract class SceneBuilderBase<TPayload> : ISceneBuilder where TPayload : IScenePayload
    {
        public ISceneState Build(IScenePayload payload) =>
            BuildState((TPayload)payload);

        protected abstract ISceneState BuildState(TPayload payload); 
    }
}