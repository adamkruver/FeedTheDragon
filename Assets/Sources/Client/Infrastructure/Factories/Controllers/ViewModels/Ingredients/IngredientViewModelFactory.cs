using System;
using System.Collections.Generic;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;
using Sources.Client.UseCases.Ingredients.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients
{
    public class IngredientViewModelFactory : IViewModelFactory<IngredientViewModel>
    {
        private readonly IReadOnlyDictionary<Type, IViewModelFactory<IngredientViewModel>> _factories;
        private readonly GetIngredientTypeQuery _getIngredientTypeQuery;
        private readonly IngredientViewModelFactoryBase _baseFactory;

        public IngredientViewModelFactory(
            IngredientViewModelFactoryBase baseFactory,
            GetIngredientTypeQuery getIngredientTypeQuery,
            IReadOnlyDictionary<Type, IViewModelFactory<IngredientViewModel>> factories
        )
        {
            _baseFactory = baseFactory;
            _factories = factories;
            _getIngredientTypeQuery = getIngredientTypeQuery;
        }

        public IViewModel Create(int id)
        {
            Type type = _getIngredientTypeQuery.Handle(id).GetType();

            return _factories.ContainsKey(type)
                ? _factories[type].Create(id)
                : _baseFactory.Create(id);
        }
    }
}