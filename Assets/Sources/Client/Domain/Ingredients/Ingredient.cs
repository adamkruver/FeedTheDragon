using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Ingredients
{
    public class Ingredient : Composite, IEntity
    {
        public Ingredient(int id, IIngredientType type)
        {
            Id = id;
            Type = type;
        }

        public IEntityType EntityType => Type;
        public IIngredientType Type { get; }
        public int Id { get; }
    }
}