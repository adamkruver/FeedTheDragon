using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Ingredients.Queries;

namespace Sources.Client.Controllers.Ingredients.Actions
{
    public class CreateIngredientSignalAction : ISignalAction<CreateIngredientSignal>
    {
        private readonly IngredientBindableViewFactory _ingredientBindableViewFactory;
        private readonly IngredientViewModelFactory _ingredientViewModelFactory;
        private readonly CreateIngredientQuery _createIngredientQuery;

        public CreateIngredientSignalAction
        (
            IngredientBindableViewFactory ingredientBindableViewFactory,
            IngredientViewModelFactory ingredientViewModelFactory,
            CreateIngredientQuery createIngredientQuery
        )
        {
            _ingredientBindableViewFactory = ingredientBindableViewFactory;
            _ingredientViewModelFactory = ingredientViewModelFactory;
            _createIngredientQuery = createIngredientQuery;
        }

        public void Handle(CreateIngredientSignal signal)
        {
            int id = _createIngredientQuery.Handle(signal.Type, signal.Position);
            IViewModel viewModel = _ingredientViewModelFactory.Create(id);
            IBindableView view = _ingredientBindableViewFactory.Create(signal.Type);

            view.Bind(viewModel);
        }
    }
}