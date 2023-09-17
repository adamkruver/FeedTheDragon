using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.Frameworks.StateMachines;

namespace Sources.Client.InfrastructureInterfaces.StateMachines
{
    public interface ISceneStateMachine : IUpdateable
    {
        void Run<T>(T payload) where T : class, IScenePayload;
        void Stop();
    }
}