using Sources.Client.Frameworks.StateMachines.Payloads;

namespace Sources.Client.Frameworks.StateMachines
{
    public interface IStateMachine<TPayload> where TPayload : IPayload
    {
        void ChangeState<T>(T payload) where T : TPayload;
    }
}