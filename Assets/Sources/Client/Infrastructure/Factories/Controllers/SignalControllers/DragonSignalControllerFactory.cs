using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Controllers;
using Sources.Client.Controllers.NPCs.Ogres.Actions;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.NPCs.Ogres.Queries;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.NPCs.Dragons.Actions;
using Sources.Client.Controllers.NPCs.Dragons.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Progresses.Components;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.NPCs.Dragons;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class DragonSignalControllerFactory
    {
        private readonly IIdGenerator _idGenerator;
        private readonly ISignalBus _signalBus;
        private readonly IEntityRepository _entityRepository;
        private readonly Environment _environment;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private QuestObserverViewModelComponentFactory _questObserverViewModelComponentFactory;

        public DragonSignalControllerFactory(
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
            CreateDragonQuery createDragonQuery = new CreateDragonQuery(_entityRepository, _idGenerator);

            DragonViewModelFactory dragonViewModelFactory = new DragonViewModelFactory
            (
                _visibilityViewModelComponentFactory,
                _positionViewModelComponentFactory
            );

            BindableViewBuilder<DragonViewModel> dragonViewBuilder =
                new BindableViewBuilder<DragonViewModel>(_bindableViewFactory, dragonViewModelFactory,
                    _environment.View["NPC"]);

            CreateDragonSignalAction createDragonSignalAction =
                new CreateDragonSignalAction(dragonViewBuilder, createDragonQuery);

            return new SignalController(
                new ISignalAction[]
                {
                    createDragonSignalAction
                });
        }
    }
}