using Sources.Client.Domain.Entities;
using Sources.Client.Domain.Ingredients;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.NPCs.Components
{
    public class QuestSlot : Composite, IEntity, IComponent
    {
        private MutableLiveData<bool> _isReached = new MutableLiveData<bool>(false);

        public QuestSlot(int id, IIngredientType requiredType)
        {
            Id = id;
            RequiredType = requiredType;
        }
        
        public int Id { get; }
        public IIngredientType RequiredType { get; }
        public LiveData<bool> IsReached => _isReached;

        public void Reach() =>
            _isReached.Value = true;
    }
}