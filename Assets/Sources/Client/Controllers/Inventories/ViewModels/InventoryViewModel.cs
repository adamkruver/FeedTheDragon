using Controllers.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;

namespace Sources.Client.Controllers.Inventories.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        public InventoryViewModel(IViewModelComponent[] components) : base(components)
        {
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }
    }
}