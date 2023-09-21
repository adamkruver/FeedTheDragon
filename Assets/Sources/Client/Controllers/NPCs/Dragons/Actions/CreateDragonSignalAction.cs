using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Controllers.NPCs.Dragons.Signals;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Dragons;

namespace Sources.Client.Controllers.NPCs.Dragons.Actions
{
    public class CreateDragonSignalAction : ISignalAction<CreateDragonSignal>
    {
        private readonly CreateDragonQuery _createDragonQuery;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly DragonViewModelFactory _dragonViewModelFactory;
        private readonly Environment _environment;

        public CreateDragonSignalAction(
            IBindableViewFactory bindableViewFactory,
            Environment environment,
            CreateDragonQuery createDragonQuery,
            DragonViewModelFactory dragonViewModelFactory
        )
        {
            _createDragonQuery = createDragonQuery;
            _bindableViewFactory = bindableViewFactory;
            _dragonViewModelFactory = dragonViewModelFactory;
            _environment = environment;
        }

        public void Handle(CreateDragonSignal signal)
        {
            int id = _createDragonQuery.Handle(signal.Position);

            IBindableView view = _bindableViewFactory.Create(_environment.View["NPC"], "Dragons/Dragon"); // TODO change to dragon
            IViewModel viewModel = _dragonViewModelFactory.Create(id);
            
            view.Bind(viewModel);
        }
    }
}