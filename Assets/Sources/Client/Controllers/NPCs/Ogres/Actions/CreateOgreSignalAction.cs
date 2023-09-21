using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Controllers.NPCs.Ogres.Signals;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels;
using Sources.Client.Domain.NPCs;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Ogres.Queries;

namespace Sources.Client.Controllers.NPCs.Ogres.Actions
{
    public class CreateOgreSignalAction : ISignalAction<CreateOgreSignal>
    {
        private readonly CreateOgreQuery _createOgreQuery;
        private readonly ISignalBus _signalBus;
        private readonly BindableViewBuilder<OgreViewModel> _bindableViewBuilder;

        public CreateOgreSignalAction(
            ISignalBus signalBus,
            BindableViewBuilder<OgreViewModel> bindableViewBuilder,
            CreateOgreQuery createOgreQuery
        )
        {
            _createOgreQuery = createOgreQuery;
            _signalBus = signalBus;
            _bindableViewBuilder = bindableViewBuilder;
        }

        public void Handle(CreateOgreSignal signal)
        {
            int id = _createOgreQuery.Handle(signal.Position);

            _bindableViewBuilder.Build(id, "Ogre");
            
            _signalBus.Handle(new CreateQuestSignal(id, 5)); //todo to level config
        }
    }
}