using System;
using Sources.Client.Domain.Entities;
using Sources.Client.Domain.Ingredients;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.Inventories
{
    public class InventorySlot : Composite, IEntity
    {
        private readonly MutableLiveData<Type> _type = new MutableLiveData<Type>();

        public InventorySlot(int id) =>
            Id = id;

        public int Id { get; }
        public Ingredient Item { get; private set; }
        public LiveData<Type> Type => _type;

        public void Set(Ingredient ingredient)
        {
            Item = ingredient;
            _type.Value = ingredient?.Type.GetType();
        }

        public void Clear() =>
            Set(null);

        public bool HasSameType(IIngredientType ingredientType)
        {
            if (Item == null)
                return false;

            return Item.Type.GetType() == ingredientType.GetType();
        }
    }
}