using System.Collections.Generic;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;

namespace Sources.Client.Controllers.Ingredients
{
    public class IngredientSignalController : SignalControllerBase
    {
        public IngredientSignalController(IEnumerable<ISignalAction> signalActions) : base(signalActions)
        {
        }
    }
}