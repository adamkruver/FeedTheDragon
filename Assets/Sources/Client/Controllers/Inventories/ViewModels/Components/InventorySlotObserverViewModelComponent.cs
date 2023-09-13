using System.Collections.Generic;
using Domain.Frameworks.Mvvm.Attributes;
using DomainInterfaces.Frameworks.Mvvm.Properties;
using PresentationInterfaces.Frameworks.Mvvm.Sources.Frameworks.Mvvm.PresentationInterfaces.Binds.AttachableViews;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.UseCases.Inventories.Queries;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.Inventories.ViewModels.Components
{
    public class InventorySlotObserverViewModelComponent : IViewModelComponent
    {
        private readonly IBindableViewBuilder<InventorySlotViewModel> _slotViewBuilder;
        private readonly LiveData<int[]> _slotIds;

        [PropertyBinding(typeof(IAttachableViewPropertyBind))]
        private IBindableProperty<IAttachableView> _view;

        private List<int> _slots = new List<int>();

        public InventorySlotObserverViewModelComponent(
            int id,
            IBindableViewBuilder<InventorySlotViewModel> slotViewBuilder,
            GetInventoryIdsQuery getInventoryIdsQuery
        )
        {
            _slotIds = getInventoryIdsQuery.Handle(id);
            _slotViewBuilder = slotViewBuilder;
        }

        public void Enable()
        {
            _slotIds.Observe(OnSlotsChanged);
        }

        public void Disable()
        {
            _slotIds.Unobserve(OnSlotsChanged);
        }

        private void OnSlotsChanged(int[] slotIds)
        {
            foreach (int slotId in slotIds)
            {
                if (_slots.Contains(slotId) == false)
                {
                    IBindableView view = _slotViewBuilder.Build(slotId, nameof(InventorySlot));
                    _view.Value.Attach(view);
                    _slots.Add(slotId);
                }
            }
        }
    }
}