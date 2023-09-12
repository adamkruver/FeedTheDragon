using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.NPCs.Common.Signals
{
    public class CreateQuestSignal : ISignal
    {
        public CreateQuestSignal(int ownerId, int tasksAmount)
        {
            OwnerId = ownerId;
            TasksAmount = tasksAmount;
        }

        public int OwnerId { get; }
        public int TasksAmount { get; }
    }
}