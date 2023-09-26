using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers;
using Sources.Client.Controllers.NPCs.Ogres.Actions;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels;
using Sources.Client.Controllers.NPCs.Ogres.ViewModels.Components;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
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
        private QuestObserverViewModelComponentFactory _questObserverViewModelComponentFactory;

        public OgreSignalControllerFactory(
            IIdGenerator idGenerator,
            ISignalBus signalBus,
            IEntityRepository entityRepository,
            Environment environment,
            IBindableViewFactory bindableViewFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            QuestObserverViewModelComponentFactory questObserverViewModelComponentFactory
        )
        {
            _idGenerator = idGenerator;
            _signalBus = signalBus;
            _entityRepository = entityRepository;
            _environment = environment;
            _bindableViewFactory = bindableViewFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _questObserverViewModelComponentFactory = questObserverViewModelComponentFactory;
        }

        public SignalController Create()
        {
            CreateOgreQuery createOgreQuery = new CreateOgreQuery(_entityRepository, _idGenerator);

            OgreViewModelFactory ogreViewModelFactory = new OgreViewModelFactory
            (
                _visibilityViewModelComponentFactory,
                _positionViewModelComponentFactory,
                _questObserverViewModelComponentFactory
            );

            BindableViewBuilder<OgreViewModel> ogreViewBuilder =
                new BindableViewBuilder<OgreViewModel>(_bindableViewFactory, ogreViewModelFactory,
                    _environment.View["NPC"]);

            CreateOgreSignalAction createOgreSignalAction =
                new CreateOgreSignalAction(_signalBus, ogreViewBuilder, createOgreQuery);

            return new SignalController(
                new ISignalAction[]
                {
                    createOgreSignalAction
                });
        }
    }
}