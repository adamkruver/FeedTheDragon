using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.UseCases.Common.Components.ComponentsListenets;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components
{
    public class QuestObserverViewModelComponentFactory
    {
        private readonly IBindableViewBuilder<QuestViewModel> _questViewBuilder;
        private readonly AddAfterComponentsChangedListnerCommand _addAfterComponentsChangedListnerCommand;
        private readonly RemoveAfterComponentsChangedListnerCommand _removeAfterComponentsChangedListnerCommand;
        private readonly GetQuestsIdsQuery _getQuestsIdsQuery;

        public QuestObserverViewModelComponentFactory
        (
            IBindableViewBuilder<QuestViewModel> questViewBuilder,
            AddAfterComponentsChangedListnerCommand addAfterComponentsChangedListnerCommand,
            RemoveAfterComponentsChangedListnerCommand removeAfterComponentsChangedListnerCommand,
            GetQuestsIdsQuery getQuestsIdsQuery
        )
        {
            _questViewBuilder = questViewBuilder;
            _addAfterComponentsChangedListnerCommand = addAfterComponentsChangedListnerCommand;
            _removeAfterComponentsChangedListnerCommand = removeAfterComponentsChangedListnerCommand;
            _getQuestsIdsQuery = getQuestsIdsQuery;
        }

        public QuestObserverViewModelComponent Create(int id)
        {
            return new QuestObserverViewModelComponent
            (
                id,
                _questViewBuilder,
                _addAfterComponentsChangedListnerCommand,
                _removeAfterComponentsChangedListnerCommand,
                _getQuestsIdsQuery
            );
        }
    }
}