using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Controllers.Characters
{
    public class CharacterSignalController : BaseSignalController
    {
        public CharacterSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}