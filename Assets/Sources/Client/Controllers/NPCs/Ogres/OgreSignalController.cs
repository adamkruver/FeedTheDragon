using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Controllers.NPCs.Ogres
{
    public class OgreSignalController : SignalControllerBase
    {
        public OgreSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}