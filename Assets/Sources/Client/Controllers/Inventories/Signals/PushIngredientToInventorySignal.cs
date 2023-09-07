using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Inventories.Signals
{
    public class PushIngredientToInventorySignal : ISignal
    {
        public PushIngredientToInventorySignal(int ingredientId)
        {
            IngredientId = ingredientId;
        }

        public int IngredientId { get; }
    }
}