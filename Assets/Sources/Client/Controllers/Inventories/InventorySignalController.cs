using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Controllers.Inventories
{
    public class InventorySignalController : SignalControllerBase
    {
        public InventorySignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}