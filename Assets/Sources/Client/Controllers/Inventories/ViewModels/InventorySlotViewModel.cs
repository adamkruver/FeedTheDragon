using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Domain.Ingredients;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using Sources.Client.UseCases.Inventories.Slots.Listeners;
using Sources.Client.UseCases.Inventories.Slots.Queries;

namespace Sources.Client.Controllers.Inventories.ViewModels
{
    public class InventorySlotViewModel : ViewModelBase
    {
        private readonly int _id;
        private readonly AddInventorySlotListener _addInventorySlotListener;
        private readonly RemoveInventorySlotListener _removeInventorySlotListener;
        private readonly GetInventorySlotItemTypeQuery _getInventorySlotItemTypeQuery;

        [PropertyBinding(typeof(IIngredientTypePropertyBind))]
        private IBindableProperty<IIngredientType> _ingredientType;

        public InventorySlotViewModel(
            IViewModelComponent[] components,
            int id,
            AddInventorySlotListener addInventorySlotListener,
            RemoveInventorySlotListener removeInventorySlotListener,
            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery
        ) : base(components)
        {
            _id = id;
            _addInventorySlotListener = addInventorySlotListener;
            _removeInventorySlotListener = removeInventorySlotListener;
            _getInventorySlotItemTypeQuery = getInventorySlotItemTypeQuery;
        }

        protected override void OnEnable()
        {
            _addInventorySlotListener.Handle(_id, OnSlotChanged);
        }

        protected override void OnDisable()
        {
            _removeInventorySlotListener.Handle(_id, OnSlotChanged);
        }

        private void OnSlotChanged()
        {
            _ingredientType.Value = _getInventorySlotItemTypeQuery.Handle(_id);
        }
    }
}