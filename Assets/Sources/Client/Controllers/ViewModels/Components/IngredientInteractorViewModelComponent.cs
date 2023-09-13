using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using Presentation.Frameworks.Mvvm.Binds.GameObjects;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class IngredientInteractorViewModelComponent : IViewModelComponent
    {
        private readonly int _playerId;
        private readonly CanPushInventoryQuery _canPushInventoryQuery;

        [PropertyBinding(typeof(GameObjectEnabledPropertyBind), "IngredientInteractor")]
        private IBindableProperty<bool> _isInteractable;

        public IngredientInteractorViewModelComponent(
            int playerId,
            CanPushInventoryQuery canPushInventoryQuery
        )
        {
            _playerId = playerId;
            _canPushInventoryQuery = canPushInventoryQuery;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        private void OnInventoryChanged() => 
            _isInteractable.Value = _canPushInventoryQuery.Handle(_playerId);
    }
}