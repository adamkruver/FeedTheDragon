using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.Ingredients.Queries
{
    public class CreateIngredientQuery
    {
        private readonly IEntityRepository _repository;
        private readonly IIngredientFactory _factory;
        private readonly IIdGenerator _idGenerator;

        public CreateIngredientQuery(
            IEntityRepository repository,
            IIngredientFactory factory,
            IIdGenerator idGenerator
        )
        {
            _repository = repository;
            _factory = factory;
            _idGenerator = idGenerator;
        }

        public int Handle(IIngredientType ingredientType, Vector3 position)
        {
            int id = _idGenerator.GetId();
            IngredientSpawnInfo spawnInfo = new IngredientSpawnInfo(position);

            Ingredient ingredient = _factory.Create(id, ingredientType, spawnInfo);
            _repository.Add(ingredient);

            return id;
        }
    }
}