using System.Collections.Generic;
using Sources.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Controllers
{
    public class CharacterSignalController : BaseSignalController
    {
        public CharacterSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}