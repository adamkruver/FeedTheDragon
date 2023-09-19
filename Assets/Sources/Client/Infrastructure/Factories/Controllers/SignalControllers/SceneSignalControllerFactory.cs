using Sources.Client.Controllers;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class SceneSignalControllerFactory
    {
        public SignalController Create()
        {
            return new SignalController(new ISignalAction[]
            {
            });
        }
    }
}