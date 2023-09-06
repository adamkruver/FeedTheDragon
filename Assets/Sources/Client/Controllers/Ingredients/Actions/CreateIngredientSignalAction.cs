﻿using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;

namespace Sources.Client.Controllers.Ingredients.Actions
{
    public class CreateIngredientSignalAction : ISignalAction<CreateIngredientSignal>
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IIngredientFactory _ingredientFactory;
        private readonly IIdGenerator _idGenerator;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly IngredientViewModelFactory _ingredientViewModelFactory;

        public CreateIngredientSignalAction
        (
            IEntityRepository entityRepository,
            IIngredientFactory ingredientFactory,
            IIdGenerator idGenerator,
            IBindableViewFactory bindableViewFactory,
            IngredientViewModelFactory ingredientViewModelFactory
        )
        {
            _entityRepository = entityRepository;
            _ingredientFactory = ingredientFactory;
            _idGenerator = idGenerator;
            _bindableViewFactory = bindableViewFactory;
            _ingredientViewModelFactory = ingredientViewModelFactory;
        }

        public void Handle(CreateIngredientSignal signal)
        {
            IIngredientType type = signal.Type;
            IngredientSpawnInfo spawnInfo = new IngredientSpawnInfo(signal.Position);

            Ingredient ingredient = _ingredientFactory.Create(_idGenerator.GetId(), type, spawnInfo);
            _entityRepository.Add(ingredient);

            IBindableView view = _bindableViewFactory.Create("", type.GetType().Name); //todo: Make constant path

            IViewModel viewModel = _ingredientViewModelFactory.Create(ingredient);

            view.Bind(viewModel);
        }
    }
}