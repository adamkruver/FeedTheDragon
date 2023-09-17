using Sources.Client.Controllers.Scenes;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class SceneSignalControllerFactory
    {
        public SceneSignalController Create()
        {
            return new SceneSignalController(new ISignalAction[]
            {
            });
        }
    }
}