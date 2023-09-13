using Sources.Client.Controllers.Ingredients.Signals;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Ingredients.Queries;

namespace Sources.Client.Controllers.Ingredients.Actions
{
    public class CreateIngredientSignalAction : ISignalAction<CreateIngredientSignal>
    {
        private readonly IBindableViewBuilder<IngredientViewModel> _viewBuilder;
        private readonly CreateIngredientQuery _createIngredientQuery;

        public CreateIngredientSignalAction
        (
            IBindableViewBuilder<IngredientViewModel> viewBuilder,
            CreateIngredientQuery createIngredientQuery
        )
        {
            _viewBuilder = viewBuilder;
            _createIngredientQuery = createIngredientQuery;
        }

        public void Handle(CreateIngredientSignal signal)
        {
            int id = _createIngredientQuery.Handle(signal.Type, signal.Position);
            _viewBuilder.Build(id, signal.Type.GetType().Name);
        }
    }
}