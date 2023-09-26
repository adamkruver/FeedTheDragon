using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;

namespace Sources.Client.Controllers.Progresses.Signals
{
    public class CreateMissionSignal : ISignal
    {
        public CreateMissionSignal(int ownerId, int requiredAmount)
        {
            OwnerId = ownerId;
            RequiredAmount = requiredAmount;
        }

        public int OwnerId { get; }
        public int RequiredAmount { get; }
    }
}