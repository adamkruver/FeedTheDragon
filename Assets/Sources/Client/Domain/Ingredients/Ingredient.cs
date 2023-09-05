using Sources.Client.Domain.Components;
using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Ingredients
{
    public class Ingredient : Enity
    {
        public readonly PositionComponent Position;
        
        public Ingredient(int id, IIngredientType type, PositionComponent position) : base(id)
        {
            Type = type;
            Position = position;
        }

        public IIngredientType Type { get; }
    }
}