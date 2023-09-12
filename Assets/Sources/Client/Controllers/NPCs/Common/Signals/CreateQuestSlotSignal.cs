using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.NPCs.Common.Signals
{
    public class CreateQuestSlotSignal : ISignal
    {
        public CreateQuestSlotSignal(int ownerId, IIngredientType type)
        {
            OwnerId = ownerId;
            Type = type;
        }

        public int OwnerId { get; }
        public IIngredientType Type { get; }
    }
}