using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;

namespace Sources.Client.InfrastructureInterfaces.StateMachines
{
    public interface ISceneStateMachine : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        void Run<T>(T payload) where T : class, IScenePayload;
        void Stop();
    }
}