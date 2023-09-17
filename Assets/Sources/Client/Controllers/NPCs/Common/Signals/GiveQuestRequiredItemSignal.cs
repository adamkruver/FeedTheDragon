using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.NPCs.Common.Signals
{
    public class GiveQuestRequiredItemSignal : ISignal
    {
        public GiveQuestRequiredItemSignal(int questSlotId) => 
            QuestSlotId = questSlotId;

        public int QuestSlotId { get; }
    }
}