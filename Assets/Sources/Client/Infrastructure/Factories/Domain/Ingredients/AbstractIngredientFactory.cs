using System;
using System.Collections.Generic;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Domain.Ingredients
{
    public class AbstractIngredientFactory : IIngredientFactory
    {
        private readonly Dictionary<Type, IIngredientFactory> _factories;
        private readonly IIngredientFactory _defaultFactory = new IngredientFactoryBase();

        public AbstractIngredientFactory()
        {
            _factories = new Dictionary<Type, IIngredientFactory>()
            {
                [typeof(ToxicFrog)] = new ToxicFrogFactory(),
            };
        }

        public Ingredient Create(int id, IIngredientType type, IngredientSpawnInfo spawnInfo) =>
            _factories.ContainsKey(type.GetType())
                ? _factories[type.GetType()].Create(id, type, spawnInfo)
                : _defaultFactory.Create(id, type, spawnInfo);
    }
}