using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers;
using Sources.Client.Controllers.Progresses.Actions;
using Sources.Client.Controllers.Progresses.ViewModels;
using Sources.Client.Domain.Progresses;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Progresses;
using Sources.Client.Infrastructure.Factories.Domain.Progresses;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Progresses;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Progresses.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class MissionSignalControllerFactory
    {
        private readonly ISignalBus _signalBus;
        private readonly EntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly QuestObserverViewModelComponentFactory _getQuestObserverViewModelComponentFactory;
        private readonly Environment _environment;

        public MissionSignalControllerFactory(
            ISignalBus signalBus,
            EntityRepository entityRepository,
            IIdGenerator idGenerator,
            IBindableViewFactory bindableViewFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            QuestObserverViewModelComponentFactory getQuestObserverViewModelComponentFactory,
            Environment environment)
        {
            _signalBus = signalBus;
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
            _bindableViewFactory = bindableViewFactory;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _getQuestObserverViewModelComponentFactory = getQuestObserverViewModelComponentFactory;
            _environment = environment;
        }

        public SignalController Create()
        {
            MissionProgressFactory missionProgressFactory = new MissionProgressFactory();
            
            CreateMissionQuery createMissionQuery = new CreateMissionQuery(_idGenerator, _entityRepository, missionProgressFactory);

            CreateMissionSignalAction createMissionSignalAction = 
                new CreateMissionSignalAction(createMissionQuery, _signalBus);
            
            return new SignalController(new ISignalAction[]
            {
                createMissionSignalAction
            });
        }
    }
}