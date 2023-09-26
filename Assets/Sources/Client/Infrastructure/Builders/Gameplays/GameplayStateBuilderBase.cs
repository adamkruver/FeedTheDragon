using Sources.Client.Controllers.Gameplays;
using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Gameplays;
using Sources.Client.Frameworks.StateMachines;

namespace Sources.Client.Infrastructure.Builders.Gameplays
{
    public abstract class GameplayStateBuilderBase<TPayload> where TPayload : IGameplayPayload
    {
        public IGameplayState Build(IStateMachine<IGameplayPayload> stateMachine, IGameplayPayload payload) =>
            BuildState(stateMachine, (TPayload)payload);

        protected abstract IGameplayState BuildState(IStateMachine<IGameplayPayload> stateMachine, TPayload payload); 
    }
}