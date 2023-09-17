using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels.Components;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Presentation.Views;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.SignalBus;
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
            IEntityRepository entityRepository,
            IBindableViewFactory bindableViewFactory,
            ISignalBus signalBus,
            SlotViewFactory slotViewFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            Environment environment,
            GetQuestsIdsQuery getQuestsIdsQuery
        )
        {
            QuestSlotViewModelFactory questSlotViewModelFactory =
                new QuestSlotViewModelFactory(visibilityViewModelComponentFactory, entityRepository, signalBus);

            BindableViewBuilder<QuestSlotViewModel> questSlotViewBuilder =
                new BindableViewBuilder<QuestSlotViewModel>(
                    slotViewFactory,
                    questSlotViewModelFactory,
                    environment.View["QuestSlot"]
                );

            QuestSlotObserverViewModelComponentFactory questSlotObserverViewModelComponentFactory =
                new QuestSlotObserverViewModelComponentFactory(entityRepository, questSlotViewBuilder);

            QuestViewModelFactory questViewModelFactory =
                new QuestViewModelFactory(visibilityViewModelComponentFactory,
                    questSlotObserverViewModelComponentFactory);

            _questViewBuilder =
                new BindableViewBuilder<QuestViewModel>(
                    bindableViewFactory,
                    questViewModelFactory,
                    environment.View["QuestSlot"]
                );            
            
            _addAfterComponentsChangedListnerCommand =
                new AddAfterComponentsChangedListnerCommand(entityRepository);
            _removeAfterComponentsChangedListnerCommand =
                new RemoveAfterComponentsChangedListnerCommand(entityRepository);

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