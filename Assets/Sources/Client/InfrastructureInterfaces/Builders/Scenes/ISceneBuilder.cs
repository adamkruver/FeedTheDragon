using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.Scenes.Payloads;

namespace Sources.Client.InfrastructureInterfaces.Builders.Scenes
{
    public interface ISceneBuilder
    {
        ISceneState Build(IScenePayload payload);
    }
}