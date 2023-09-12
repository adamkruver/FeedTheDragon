using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.NPCs.Components;

namespace Sources.Client.Infrastructure.Factories.Domain.NPCs
{
    public class QuestSlotFactory
    {
        public QuestSlot Create(int id, IIngredientType requiredType)
        {
            QuestSlot questSlot = new QuestSlot(id, requiredType);
            
            questSlot.AddComponent(new VisibilityComponent(true));
            
            return questSlot;
        }
    }
}