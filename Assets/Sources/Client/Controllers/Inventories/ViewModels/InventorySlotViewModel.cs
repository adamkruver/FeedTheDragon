using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using Sources.Client.UseCases.Inventories.Slots.Queries;
using Utils.LiveData;

namespace Sources.Client.Controllers.Inventories.ViewModels
{
    public class InventorySlotViewModel : ViewModelBase
    {
        private readonly LiveData<Type> _itemType; 

        [PropertyBinding(typeof(IIngredientTypePropertyBind))]
        private IBindableProperty<Type> _ingredientType;

        public InventorySlotViewModel(
            IViewModelComponent[] components,
            int id,
            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery
        ) : base(components)
        {
            _itemType = getInventorySlotItemTypeQuery.Handle(id);
        }

        protected override void OnEnable()
        {
            _itemType.Observe(OnSlotChanged);
        }

        protected override void OnDisable()
        {
            _itemType.Unobserve(OnSlotChanged);
        }

        private void OnSlotChanged(Type itemType)
        {
            _ingredientType.Value = itemType;
        }
    }
}