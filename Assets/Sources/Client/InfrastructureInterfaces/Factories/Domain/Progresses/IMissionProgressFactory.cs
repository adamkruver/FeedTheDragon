using Sources.Client.Domain.Progresses;

namespace Sources.Client.InfrastructureInterfaces.Factories.Domain.Progresses
{
    public interface IMissionProgressFactory
    {
        public Mission Create(int id, int requiredAmount);
    }
}