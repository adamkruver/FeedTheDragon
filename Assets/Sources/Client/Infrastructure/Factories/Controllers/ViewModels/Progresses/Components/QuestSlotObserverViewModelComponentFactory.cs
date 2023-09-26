using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels.Components;
using Sources.Client.Domain.Entities;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components
{
    public class QuestSlotObserverViewModelComponentFactory
    {
        private readonly GetQuestSlotsIdsQuery _getQuestSlotsIdsQuery;
        private readonly IBindableViewBuilder<QuestSlotViewModel> _questSlotViewBuilder;

        public QuestSlotObserverViewModelComponentFactory
        (
            IEntityRepository entityRepository,
            IBindableViewBuilder<QuestSlotViewModel> questSlotViewBuilder
        )
        {
            _getQuestSlotsIdsQuery = new GetQuestSlotsIdsQuery(entityRepository);
            _questSlotViewBuilder = questSlotViewBuilder;
        }

        public IViewModelComponent Create(int id) =>
            new QuestSlotObserverViewModelComponent
            (
                id,
                _getQuestSlotsIdsQuery,
                _questSlotViewBuilder
            );
    }
}