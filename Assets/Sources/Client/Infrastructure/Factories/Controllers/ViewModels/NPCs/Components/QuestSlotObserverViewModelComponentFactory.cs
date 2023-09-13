using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.NPCs.Common.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components
{
    public class QuestSlotObserverViewModelComponentFactory
    {
        private readonly GetQuestSlotsIdsQuery _getQuestSlotsIdsQuery;
        private readonly IBindableViewBuilder<QuestSlotViewModel> _questSlotViewBuilder;

        public QuestSlotObserverViewModelComponentFactory
        (
            GetQuestSlotsIdsQuery getQuestSlotsIdsQuery,
            IBindableViewBuilder<QuestSlotViewModel> questSlotViewBuilder
        )
        {
            _getQuestSlotsIdsQuery = getQuestSlotsIdsQuery;
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