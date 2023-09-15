using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Common.ViewModels;
using Sources.Client.Controllers.NPCs.Ogres;
using Sources.Client.Controllers.NPCs.Ogres.Actions;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components;
using Sources.Client.Infrastructure.Factories.Presentation.Views;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Common.Components.ComponentsListenets;
using Sources.Client.UseCases.NPCs.Common.Quests.Queries;
using Sources.Client.UseCases.NPCs.Ogres.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class OgreSignalControllerFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly ISignalBus _signalBus;
        private readonly IEntityRepository _entityRepository;
        private readonly Environment _environment;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly IBindableViewFactory _slotViewFactory;

        public OgreSignalControllerFactory(
            IIdGenerator idGenerator,
            ISignalBus signalBus,
            IEntityRepository entityRepository,
            Environment environment,
            IBindableViewFactory bindableViewFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory, 
            IBindableViewFactory slotViewFactory
            )
        {
            _idGenerator = idGenerator;
            _signalBus = signalBus;
            _entityRepository = entityRepository;
            _environment = environment;
            _bindableViewFactory = bindableViewFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _slotViewFactory = slotViewFactory;
        }
        
        public OgreSignalController Create()
        {
            CreateOgreQuery createOgreQuery = new CreateOgreQuery(_entityRepository, _idGenerator);
            
            QuestSlotViewModelFactory questSlotViewModelFactory =
                new QuestSlotViewModelFactory(_visibilityViewModelComponentFactory, _entityRepository);

            BindableViewBuilder<QuestSlotViewModel> questSlotViewBuilder =
                new BindableViewBuilder<QuestSlotViewModel>(
                    _slotViewFactory,
                    questSlotViewModelFactory,
                    _environment.View["QuestSlot"]
                );
            
            GetQuestSlotsIdsQuery getQuestSlotsIdsQuery = new GetQuestSlotsIdsQuery(_entityRepository);

            QuestSlotObserverViewModelComponentFactory questSlotObserverViewModelComponentFactory =
                new QuestSlotObserverViewModelComponentFactory(getQuestSlotsIdsQuery, questSlotViewBuilder);
            
            QuestViewModelFactory questViewModelFactory =
                new QuestViewModelFactory(_visibilityViewModelComponentFactory,
                    questSlotObserverViewModelComponentFactory);

            BindableViewBuilder<QuestViewModel> questViewBuilder =
                new BindableViewBuilder<QuestViewModel>(
                    _bindableViewFactory,
                    questViewModelFactory,
                    _environment.View["QuestSlot"]
                );

            AddAfterComponentsChangedListnerCommand addAfterComponentsChangedListnerCommand =
                new AddAfterComponentsChangedListnerCommand(_entityRepository);
            
            RemoveAfterComponentsChangedListnerCommand removeAfterComponentsChangedListnerCommand =
                new RemoveAfterComponentsChangedListnerCommand(_entityRepository);
            
            GetQuestsIdsQuery getQuestsIdsQuery = new GetQuestsIdsQuery(_entityRepository);

            QuestObserverViewModelComponentFactory questObserverViewModelComponentFactory =
                new QuestObserverViewModelComponentFactory
                (
                    questViewBuilder,
                    addAfterComponentsChangedListnerCommand,
                    removeAfterComponentsChangedListnerCommand,
                    getQuestsIdsQuery
                );

            OgreViewModelFactory ogreViewModelFactory = new OgreViewModelFactory
            (
                _visibilityViewModelComponentFactory,
                _positionViewModelComponentFactory,
                questObserverViewModelComponentFactory
            );

            CreateOgreSignalAction createOgreSignalAction =
                new CreateOgreSignalAction(_signalBus, _bindableViewFactory, _environment, createOgreQuery,
                    ogreViewModelFactory);

            return new OgreSignalController(
                new ISignalAction[]
                {
                    createOgreSignalAction
                });
        }
    }
}