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

        public CreateMissionSignalAction(
            CreateMissionQuery createMissionQuery,
            ISignalBus signalBus
        )
        {
            _createMissionQuery = createMissionQuery;
            _signalBus = signalBus;
        }

        public void Handle(CreateMissionSignal signal)
        {
            int id = _createMissionQuery.Handle(signal.OwnerId, signal.RequiredAmount);

            for (int i = 0; i < signal.RequiredAmount; i++)
            {
                _signalBus.Handle(new CreateQuestSignal(id, 3)); //todo: to level config
            }
        }
    }
}