namespace Sources.Client.Domain.Scenes.Payloads
{
    public class InitialPayload:IScenePayload
    {
        public string SceneName { get; } = "InitialScene";
    }
}