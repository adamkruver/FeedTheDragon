using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients;
using UnityEngine;

namespace Sources.Client.Infrastructure.Factories.Domain.Ingredients
{
    public class IngredientFactory : IIngredientFactory
    {
        public Ingredient Create(int id, IIngredientType type, Vector3 position)
        {
            return new Ingredient(id, type, new PositionComponent(position));
        }
    }
}