using Sources.Client.Controllers.Scenes.StateMachines.States;
using Sources.Client.Domain.AppStates.Payloads;

namespace Sources.Client.InfrastructureInterfaces.Builders.Scenes
{
    public interface ISceneBuilder
    {
        ISceneState Build(IScenePayload payload);
    }
}