using Sources.Client.Domain.Components;
using Sources.Client.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Progresses;

namespace Sources.Client.Infrastructure.Factories.Domain.Progresses
{
    public class MissionProgressFactory : IMissionProgressFactory
    {
        public Mission Create(int id, int requiredAmount)
        {
            Mission mission = new Mission(id, requiredAmount);
            
            mission.AddComponent(new VisibilityComponent(true));
            
            return mission;
        }
    }
}