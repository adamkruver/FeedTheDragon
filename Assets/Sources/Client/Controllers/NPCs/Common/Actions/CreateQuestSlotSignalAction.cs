using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Controllers.NPCs.Common.Actions
{
    public class CreateQuestSlotSignalAction : ISignalAction<CreateQuestSlotSignal>
    {
        private readonly CreateQuestSlotQuery _createQuestSlotQuery;

        public CreateQuestSlotSignalAction
        (
            CreateQuestSlotQuery createQuestSlotQuery
        )
        {
            _createQuestSlotQuery = createQuestSlotQuery;
        }

        public void Handle(CreateQuestSlotSignal signal)
        {
            _createQuestSlotQuery.Handle(signal.Type, signal.QuestId);
        }
    }
}