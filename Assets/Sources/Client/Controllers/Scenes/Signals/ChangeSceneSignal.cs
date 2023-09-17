using Sources.Client.Domain.Scenes.Payloads;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Scenes.Signals
{
    public class ChangeSceneSignal : ISignal
    {
        public ChangeSceneSignal(IScenePayload scenePayload)
        {
            ScenePayload = scenePayload;
        }
        
        public IScenePayload ScenePayload { get; }
    }
}