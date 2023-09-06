using Sources.Client.Domain.Ingredients;
using UnityEngine;

namespace Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients
{
    public interface IIngredientFactory
    {
        Ingredient Create(int id, IIngredientType type, IngredientSpawnInfo spawnInfo);
    }
}