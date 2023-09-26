using Sources.Client.Controllers.Progresses.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.UseCases.Progresses.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class MissionCounterViewModelComponentFactory
    {
        private readonly IEntityRepository _entityRepository;

        public MissionCounterViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public MissionCounterViewModelComponent Create(int id)
        {
            GetMissionQuestCompletedAmountQuery getMissionQuestCompletedAmountQuery =
                new GetMissionQuestCompletedAmountQuery(_entityRepository);

            GetMissionQuestRequiredAmountQuery getMissionQuestRequiredAmountQuery =
                new GetMissionQuestRequiredAmountQuery(_entityRepository);

            return new MissionCounterViewModelComponent(id, getMissionQuestCompletedAmountQuery,
                getMissionQuestRequiredAmountQuery);
        }
    }
}