using PresentationInterfaces.Frameworks.Mvvm.Views;
using UnityEngine;

namespace Sources.Client.PresentationInterfaces.Views
{
    public interface IInventorySlotView : IBindableView
    {
        void SetParent(Transform parent);
    }
}