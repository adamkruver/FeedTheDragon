using Sources.Client.Frameworks.StateMachines.Payloads;

namespace Sources.Client.Domain.AppStates.Payloads
{
    public interface IScenePayload : IPayload
    {
        string SceneName { get; }
    }
}