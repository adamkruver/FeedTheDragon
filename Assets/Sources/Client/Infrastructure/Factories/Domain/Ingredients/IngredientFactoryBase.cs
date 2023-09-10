using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Domain.Ingredients
{
    public class IngredientFactoryBase : IIngredientFactory
    {
        public Ingredient Create(int id, IIngredientType type, IngredientSpawnInfo spawnInfo)
        {
            Ingredient ingredient = new Ingredient(id, type);

            AddBaseComponents(ingredient, spawnInfo);
            AddComponents(ingredient, spawnInfo);

            return ingredient;
        }

        protected void AddBaseComponents(Ingredient ingredient, IngredientSpawnInfo spawnInfo)
        {
            ingredient.AddComponent(new VisibilityComponent(true));
            ingredient.AddComponent(new PositionComponent(spawnInfo.Position));
        }

        protected virtual void AddComponents(Ingredient ingredient, IngredientSpawnInfo spawnInfo)
        {
        }
    }
}