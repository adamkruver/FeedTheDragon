using System;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Ingredients.Queries
{
    public class GetIngredientTypeQuery
    {
        private readonly IEntityRepository _repository;

        public GetIngredientTypeQuery(IEntityRepository repository)
        {
            _repository = repository;
        }

        public IIngredientType Handle(int id)
        {
            if (_repository.Get(id) is not Ingredient ingredient)
                throw new InvalidCastException();

            return ingredient.Type;
        } 
    }
}