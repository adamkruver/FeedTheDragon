using Sources.Client.Frameworks.StateMachines.Payloads;

namespace Sources.Client.Domain.Scenes.Payloads
{
    public interface IScenePayload : IPayload
    {
        string SceneName { get; }
    }
}