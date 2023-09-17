using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Controllers.Scenes
{
    public class SceneSignalController : SignalControllerBase
    {
        public SceneSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}