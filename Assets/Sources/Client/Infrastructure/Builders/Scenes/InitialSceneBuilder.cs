using Sources.Client.Controllers.Scenes.Initial;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;

namespace Sources.Client.Infrastructure.Builders.Scenes
{
    public class InitialSceneBuilder : SceneBuilderBase<InitialPayload>
    {
        protected override ISceneState BuildState(IStateMachine<IScenePayload> stateMachine, InitialPayload payload)
        {
            return new InitialScene(stateMachine);
        }
    }
}