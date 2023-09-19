using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers;
using Sources.Client.Controllers.Enemies.Spiders.Actions;
using Sources.Client.Controllers.Enemies.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Enemies;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Enemies.Spiders.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class EnemySignalControllerFactory
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly Environment _environment;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;

        public EnemySignalControllerFactory(
            IEntityRepository entityRepository,
            IIdGenerator idGenerator,
            IBindableViewFactory bindableViewFactory,
            Environment environment,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory
        )
        {
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
            _bindableViewFactory = bindableViewFactory;
            _environment = environment;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
        }

        public SignalController Create()
        {
            CreateSpiderQuery createSpiderQuery = new CreateSpiderQuery(_entityRepository, _idGenerator);


            SpiderViewModelFactory spiderViewModelFactory = new SpiderViewModelFactory(
                _positionViewModelComponentFactory,
                _visibilityViewModelComponentFactory
            );

            BindableViewBuilder<EnemyViewModel> spiderViewBuilder = new BindableViewBuilder<EnemyViewModel>(
                _bindableViewFactory,
                spiderViewModelFactory,
                _environment.View["Enemy"]
            );

            CreateSpiderSignalAction createSpiderSignalAction =
                new CreateSpiderSignalAction(createSpiderQuery, spiderViewBuilder);

            return new SignalController(
                new ISignalAction[]
                {
                    createSpiderSignalAction,
                }
            );
        }
    }
}