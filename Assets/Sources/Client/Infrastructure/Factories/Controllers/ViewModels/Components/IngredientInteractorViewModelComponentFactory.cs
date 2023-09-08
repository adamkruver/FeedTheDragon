using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.UseCases.Inventories.Listeners;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class IngredientInteractorViewModelComponentFactory
    {
        private readonly AddInventoryListener _addInventoryListener;
        private readonly RemoveInventoryListener _removeInventoryListener;
        private readonly CanPushInventoryQuery _canPushInventoryQuery;

        public IngredientInteractorViewModelComponentFactory(
            AddInventoryListener addInventoryListener,
            RemoveInventoryListener removeInventoryListener,
            CanPushInventoryQuery canPushInventoryQuery
        )
        {
            _addInventoryListener = addInventoryListener;
            _removeInventoryListener = removeInventoryListener;
            _canPushInventoryQuery = canPushInventoryQuery;
        }

        public IngredientInteractorViewModelComponent Create(int playerId) =>
            new IngredientInteractorViewModelComponent(
                playerId,
                _addInventoryListener,
                _removeInventoryListener,
                _canPushInventoryQuery
            );
    }
}