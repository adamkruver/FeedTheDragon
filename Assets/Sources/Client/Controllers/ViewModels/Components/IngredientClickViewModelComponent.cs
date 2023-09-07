using Domain.Frameworks.Mvvm.Attributes;
using PresentationInterfaces.Frameworks.Mvvm.Binds.Buttons;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using UnityEngine;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class IngredientClickViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly ISignalBus _signalBus;

        public IngredientClickViewModelComponent(int id, ISignalBus signalBus)
        {
            _id = id;
            _signalBus = signalBus;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
        }
        
        [MethodBinding(typeof(IButtonClickMethodBind))]
        private void OnClick(Vector3 position)
        {
            _signalBus.Handle(new PushIngredientToInventorySignal(_id));
        }
    }
}