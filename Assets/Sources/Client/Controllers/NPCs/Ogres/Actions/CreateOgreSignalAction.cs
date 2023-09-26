using Sources.Client.Controllers.NPCs.Ogres.Signals;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels;
using Sources.Client.Controllers.Progresses.Signals;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
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

            _signalBus.Handle(new CreateMissionSignal(id, 3)); //todo to level config

            _bindableViewBuilder.Build(id, "Ogre");
        }
    }
}