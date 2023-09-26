using Sources.Client.Controllers.NPCs.Common.Signals;
using Sources.Client.Controllers.Progresses.Signals;
using Sources.Client.Controllers.Progresses.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using Sources.Client.UseCases.Progresses.Queries;

namespace Sources.Client.Controllers.Progresses.Actions
{
    public class CreateMissionSignalAction : ISignalAction<CreateMissionSignal>
    {
        private readonly CreateMissionQuery _createMissionQuery;

        private readonly ISignalBus _signalBus;
        // private readonly BindableViewBuilder<MissionViewModel> _bindableViewBuilder;

        public CreateMissionSignalAction(
            CreateMissionQuery createMissionQuery,
            ISignalBus signalBus
            //BindableViewBuilder<MissionViewModel> bindableViewBuilder
        )
        {
            _createMissionQuery = createMissionQuery;
            _signalBus = signalBus;
            //_bindableViewBuilder = bindableViewBuilder;
        }

        public void Handle(CreateMissionSignal signal)
        {
            int id = _createMissionQuery.Handle(signal.OwnerId, signal.RequiredAmount);

            //_bindableViewBuilder.Build(id, "Mission");

            for (int i = 0; i < signal.RequiredAmount; i++)
            {
                _signalBus.Handle(new CreateQuestSignal(id, 3)); //todo: to level config
            }
        }
    }
}