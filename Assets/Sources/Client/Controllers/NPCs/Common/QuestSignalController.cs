using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Controllers.NPCs.Common
{
    public class QuestSignalController : SignalControllerBase
    {
        public QuestSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}