using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Frameworks.StateMachines.Payloads;

namespace Sources.Client.InfrastructureInterfaces.StateMachines.Generic
{
    public interface ISceneStateMachine<TPayload> : IStateMachine<TPayload>, ISceneStateMachine
        where TPayload : IPayload
    {
    }
}