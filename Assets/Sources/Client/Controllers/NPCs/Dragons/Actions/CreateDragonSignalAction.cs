using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Controllers.NPCs.Dragons.Signals;
using Sources.Client.Controllers.NPCs.Dragons.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Dragons;

namespace Sources.Client.Controllers.NPCs.Dragons.Actions
{
    public class CreateDragonSignalAction : ISignalAction<CreateDragonSignal>
    {
        private readonly BindableViewBuilder<DragonViewModel> _bindableViewBuilder;
        private readonly CreateDragonQuery _createDragonQuery;

        public CreateDragonSignalAction(
            BindableViewBuilder<DragonViewModel> bindableViewBuilder,
            CreateDragonQuery createDragonQuery 
        )
        {
            _bindableViewBuilder = bindableViewBuilder;
            _createDragonQuery = createDragonQuery;
        }

        public void Handle(CreateDragonSignal signal)
        {
            int id = _createDragonQuery.Handle(signal.Position);
            _bindableViewBuilder.Build(id, "Dragon");
        }
    }
}