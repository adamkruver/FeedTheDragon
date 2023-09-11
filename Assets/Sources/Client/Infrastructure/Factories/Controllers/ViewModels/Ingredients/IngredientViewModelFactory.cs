using System;
using System.Collections.Generic;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients
{
    public class IngredientViewModelFactory
    {
        private readonly IReadOnlyDictionary<Type, IIngredientViewModelFactory> _factories;
        private readonly IngredientViewModelFactoryBase _baseFactory;

        public IngredientViewModelFactory(
            IngredientViewModelFactoryBase baseFactory,
            IReadOnlyDictionary<Type, IIngredientViewModelFactory> factories)
        {
            _baseFactory = baseFactory;
            _factories = factories;
        }

        public IViewModel Create(int id, IIngredientType type) =>
            _factories.ContainsKey(type.GetType())
                ? _factories[type.GetType()].Create(id)
                : _baseFactory.Create(id);
    }
}