using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.SignalBus;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class IngredientClickViewModelComponentFactory // todo: rename to Pickup?
    {
        private readonly ISignalBus _signalBus;

        public IngredientClickViewModelComponentFactory(ISignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public IngredientClickViewModelComponent Create(int id)
        {
            return new IngredientClickViewModelComponent(id, _signalBus);
        }
    }
}