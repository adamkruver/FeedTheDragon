using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Common.Quests.Commands;

namespace Sources.Client.Controllers.NPCs.Common.Actions
{
    public class GiveQuestRequiredItemSignalAction : ISignalAction<GiveQuestRequiredItemSignal>
    {
        private readonly GiveQuestRequiredItemCommand _giveQuestRequiredItemCommand;

        public GiveQuestRequiredItemSignalAction(GiveQuestRequiredItemCommand giveQuestRequiredItemCommand) => 
            _giveQuestRequiredItemCommand = giveQuestRequiredItemCommand;

        public void Handle(GiveQuestRequiredItemSignal signal) => 
            _giveQuestRequiredItemCommand.Handle(signal.QuestSlotId);
    }
}