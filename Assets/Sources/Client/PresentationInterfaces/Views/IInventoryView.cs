using PresentationInterfaces.Frameworks.Mvvm.Views;

namespace Sources.Client.PresentationInterfaces.Views
{
    public interface IInventoryView : IBindableView
    {
        public void Add(IInventorySlotView slotView);
        public void Remove(IInventorySlotView slotView);
    }
}