using Sources.Client.Domain.NPCs.Components;

namespace Sources.Client.InfrastructureInterfaces.Factories.Domain.NPCs
{
    public interface IQuestFactory
    {
        public Quest Create(int id);
    }
}