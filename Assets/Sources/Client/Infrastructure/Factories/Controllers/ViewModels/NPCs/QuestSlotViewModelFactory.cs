using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs
{
    public class QuestSlotViewModelFactory : IViewModelFactory<QuestSlotViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly ISignalBus _signalBus;
        private readonly GetQuestSlotRequiredTypeQuery _getQuestSlotRequiredTypeQuery;
        private readonly GetQuestSlotIsReachedQuery _getQuestSlotIsReachedQuery;

        public QuestSlotViewModelFactory(
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            IEntityRepository entityRepository,
            ISignalBus signalBus
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _signalBus = signalBus;
            _getQuestSlotRequiredTypeQuery = new GetQuestSlotRequiredTypeQuery(entityRepository);
            _getQuestSlotIsReachedQuery = new GetQuestSlotIsReachedQuery(entityRepository);
        }

        public IViewModel Create(int id) =>
            new QuestSlotViewModel(new IViewModelComponent[]
                {
                    _visibilityViewModelComponentFactory.Create(id)
                },
                id,
                _signalBus,
                _getQuestSlotRequiredTypeQuery,
                _getQuestSlotIsReachedQuery
            );
    }
}