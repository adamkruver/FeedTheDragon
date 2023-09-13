using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.NPCs.Common.Signals
{
    public class CreateQuestSlotSignal : ISignal
    {
        public CreateQuestSlotSignal(int questId, IIngredientType type)
        {
            QuestId = questId;
            Type = type;
        }

        public int QuestId { get; }
        public IIngredientType Type { get; }
    }
}