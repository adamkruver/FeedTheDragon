using System.Collections.Generic;
using Sources.Infrastructure.SignalBus.Interfaces;

namespace Sources.Infrastructure.SignalBus
{
    public class CharacterSignalController : BaseSignalController
    {
        public CharacterSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}