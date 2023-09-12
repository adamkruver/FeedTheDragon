using Sources.Client.Domain.Components;
using Sources.Client.Domain.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.NPCs;

namespace Sources.Client.Infrastructure.Factories.Domain.NPCs
{
    public class QuestFactory : IQuestFactory
    {
        public Quest Create(int id)
        {
            Quest quest = new Quest(id);
            
            quest.AddComponent(new VisibilityComponent(true));
            
            return quest;
        }
    }
}