using Sources.Client.Domain.AppStates.Payloads;

namespace Sources.Client.Frameworks.StateMachines.Payloads
{
    public class GameplayPayload : IScenePayload
    {
        public string SceneName { get; } = "GameplayScene";
    }
}