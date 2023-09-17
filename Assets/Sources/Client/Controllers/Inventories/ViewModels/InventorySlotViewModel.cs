using System;
using Controllers.Frameworks.Mvvm.ViewModels;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Buttons;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.PresentationInterfaces.Binds.Ingredients;
using Sources.Client.UseCases.Inventories.Slots.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.Inventories.ViewModels
{
    public class InventorySlotViewModel : ViewModelBase
    {
        private readonly int _id;
        private readonly ISignalBus _signalBus;
        private readonly LiveData<Type> _itemType;

        [PropertyBinding(typeof(IIngredientTypePropertyBind))]
        private IBindableProperty<Type> _ingredientType;

        public InventorySlotViewModel(
            IViewModelComponent[] components,
            int id,
            ISignalBus signalBus,
            GetInventorySlotItemTypeQuery getInventorySlotItemTypeQuery
        ) : base(components)
        {
            _id = id;
            _signalBus = signalBus;
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

        [MethodBinding(typeof(IButtonClickMethodBind))]
        private void OnClick(Vector3 position) =>
            _signalBus.Handle(new DropInventoryItemSignal(_id));
    }
}