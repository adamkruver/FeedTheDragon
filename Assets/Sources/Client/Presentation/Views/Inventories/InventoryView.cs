using Presentation.Frameworks.Mvvm.Views;
using Sources.Client.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Client.Presentation.Views.Inventories
{
    public class InventoryView : BindableView, IInventoryView
    {
        [SerializeField] private Transform _slotParent;

        public void Add(IInventorySlotView slotView) => 
            slotView.SetParent(_slotParent);

        public void Remove(IInventorySlotView slotView) => 
            slotView.SetParent(null);
    }
}