using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.UseCases.InventoryComponents.Listeners;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class InventoryChangedViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly AddInventoryListener _addInventoryListener;
        private readonly RemoveInventoryListener _removeInventoryListener;

        public InventoryChangedViewModelComponent(
            int id,
            AddInventoryListener addInventoryListener,
            RemoveInventoryListener removeInventoryListener
        )
        {
            _id = id;
            _addInventoryListener = addInventoryListener;
            _removeInventoryListener = removeInventoryListener;
        }

        public void Enable()
        {
            _addInventoryListener.Handle(_id, OnInventoryChanged);
        }

        public void Disable()
        {
            _removeInventoryListener.Handle(_id, OnInventoryChanged);
        }

        private void OnInventoryChanged()
        {
            Debug.Log("Inventory changed");
        }
    }
}