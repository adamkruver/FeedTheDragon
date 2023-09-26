using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Progresses.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Progresses
{
    public class QuestViewModelFactory : IViewModelFactory<QuestViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly QuestSlotObserverViewModelComponentFactory _questSlotObserverViewModelComponentFactory;
        private readonly GetQuestIsCompletedQuery _getQuestIsCompletedQuery;

        public QuestViewModelFactory
        (
            IEntityRepository entityRepository,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            QuestSlotObserverViewModelComponentFactory questSlotObserverViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _questSlotObserverViewModelComponentFactory = questSlotObserverViewModelComponentFactory;

            _getQuestIsCompletedQuery = new GetQuestIsCompletedQuery(entityRepository);
        }

        public IViewModel Create(int id) =>
            new QuestViewModel(
                new IViewModelComponent[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                    _questSlotObserverViewModelComponentFactory.Create(id)
                },
                id,
                _getQuestIsCompletedQuery
            );
    }
}