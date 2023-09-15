using System;
using System.Collections.Generic;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Controllers.Ingredients;
using Sources.Client.Controllers.Ingredients.Actions;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients.Components;
using Sources.Client.Infrastructure.Factories.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Common.Components.Destinations.Commands;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;
using Sources.Client.UseCases.Ingredients.Queries;
using Environment = Sources.Client.App.Configs.Environment;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class IngredientSignalControllerFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly Environment _environment;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly ISignalBus _signalBus;
        private readonly MoveToDestinationViewModelComponentFactory _moveToDestinationViewModelComponentFactory;
        private readonly IIdGenerator _idGenerator;

        public IngredientSignalControllerFactory(
            IEntityRepository entityRepository, 
            Environment environment,
            IBindableViewFactory bindableViewFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            ISignalBus signalBus,
            MoveToDestinationViewModelComponentFactory moveToDestinationViewModelComponentFactory,
            IIdGenerator idGenerator)
        {
            _entityRepository = entityRepository;
            _environment = environment;
            _bindableViewFactory = bindableViewFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _signalBus = signalBus;
            _moveToDestinationViewModelComponentFactory = moveToDestinationViewModelComponentFactory;
            _idGenerator = idGenerator;
        }

        public IngredientSignalController Create()
        {
            AbstractIngredientFactory ingredientFactory = new AbstractIngredientFactory();

            CreateIngredientQuery createIngredientQuery =
                new CreateIngredientQuery(_entityRepository, ingredientFactory, _idGenerator);

            GetIngredientTypeQuery getIngredientTypeQuery = new GetIngredientTypeQuery(_entityRepository);

            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory =
                new LookDirectionViewModelComponentFactory(_entityRepository);

            IngredientClickViewModelComponentFactory ingredientClickViewModelComponentFactory =
                new IngredientClickViewModelComponentFactory(_signalBus);

            IngredientViewModelFactoryBase ingredientViewModelFactoryBase = new IngredientViewModelFactoryBase(
                _visibilityViewModelComponentFactory,
                _positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory
            );

            ToxicFrogBehaviourTreeViewModelComponentFactory toxicFrogBehaviourTreeViewModelComponentFactory =
                new ToxicFrogBehaviourTreeViewModelComponentFactory(_signalBus, _entityRepository);

            ToxicFrogViewModelFactory toxicFrogViewModelFactory = new ToxicFrogViewModelFactory(
                _visibilityViewModelComponentFactory,
                _positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory,
                _moveToDestinationViewModelComponentFactory,
                toxicFrogBehaviourTreeViewModelComponentFactory,
                lookDirectionViewModelComponentFactory
            );

            IViewModelFactory<IngredientViewModel> ingredientViewModelFactory = new IngredientViewModelFactory(
                ingredientViewModelFactoryBase,
                getIngredientTypeQuery,
                new Dictionary<Type, IViewModelFactory<IngredientViewModel>>()
                {
                    [typeof(ToxicFrog)] = toxicFrogViewModelFactory
                }
            );

            BindableViewBuilder<IngredientViewModel> ingredientViewBuilder =
                new BindableViewBuilder<IngredientViewModel>(
                    _bindableViewFactory,
                    ingredientViewModelFactory,
                    _environment.View["Ingredient"]
                );

            CreateIngredientSignalAction createIngredientSignalAction = new CreateIngredientSignalAction(
                ingredientViewBuilder,
                createIngredientQuery
            );

            SetDestinationCommand setDestinationCommand = new SetDestinationCommand(_entityRepository);

            SetSpeedCommand setSpeedCommand = new SetSpeedCommand(_entityRepository);

            ToxicFrogJumpSignalAction toxicFrogJumpSignalAction =
                new ToxicFrogJumpSignalAction(setDestinationCommand, setSpeedCommand);

            return new IngredientSignalController(
                new ISignalAction[]
                {
                    createIngredientSignalAction,
                    toxicFrogJumpSignalAction
                });
        }
    }
}