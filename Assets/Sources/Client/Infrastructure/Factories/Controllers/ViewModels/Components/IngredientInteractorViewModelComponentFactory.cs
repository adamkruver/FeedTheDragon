using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class IngredientInteractorViewModelComponentFactory
    {
        private readonly CanPushInventoryQuery _canPushInventoryQuery;

        public IngredientInteractorViewModelComponentFactory(
            CanPushInventoryQuery canPushInventoryQuery
        )
        {
            _canPushInventoryQuery = canPushInventoryQuery;
        }

        public IngredientInteractorViewModelComponent Create(int playerId) =>
            new IngredientInteractorViewModelComponent(
                playerId,
                _canPushInventoryQuery
            );
    }
}