using System;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using Presentation.Frameworks.Mvvm.Binds.GameObjects;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.Inventories.Listeners;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class IngredientInteractorViewModelComponent : IViewModelComponent
    {
        private readonly int _playerId;
        private readonly AddInventoryListener _addInventoryListener;
        private readonly RemoveInventoryListener _removeInventoryListener;
        private readonly CanPushInventoryQuery _canPushInventoryQuery;

        [PropertyBinding(typeof(GameObjectEnabledPropertyBind), "IngredientInteractor")]
        private IBindableProperty<bool> _isInteractable;

        public IngredientInteractorViewModelComponent(
            int playerId,
            AddInventoryListener addInventoryListener,
            RemoveInventoryListener removeInventoryListener,
            CanPushInventoryQuery canPushInventoryQuery
        )
        {
            _playerId = playerId;
            _addInventoryListener = addInventoryListener;
            _removeInventoryListener = removeInventoryListener;
            _canPushInventoryQuery = canPushInventoryQuery;
        }

        public void Enable() => 
            _addInventoryListener.Handle(_playerId, OnInventoryChanged);

        public void Disable() => 
            _removeInventoryListener.Handle(_playerId, OnInventoryChanged);

        private void OnInventoryChanged() => 
            _isInteractable.Value = _canPushInventoryQuery.Handle(_playerId);
    }
}