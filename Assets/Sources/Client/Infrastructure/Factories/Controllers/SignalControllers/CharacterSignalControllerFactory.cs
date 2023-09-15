using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.App.Configs;
using Sources.Client.Controllers.Characters;
using Sources.Client.Controllers.Characters.Actions;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Infrastructure.Builders.Presentation.BindableViews;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Domain.Characters;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions;
using Sources.Client.UseCases.Characters.Queries;
using Sources.Client.UseCases.Common.Components.LookDirection.Commands;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;
using Sources.Client.UseCases.Inventories.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.SignalControllers
{
    public class CharacterSignalControllerFactory
    {
        private readonly CurrentPlayerService _currentPlayerService;
        private readonly ISignalBus _signalBus;
        private readonly CameraFollowService _cameraFollowService;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly Environment _environment;
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;

        public CharacterSignalControllerFactory(CurrentPlayerService currentPlayerService, ISignalBus signalBus,
            CameraFollowService cameraFollowService, IBindableViewFactory bindableViewFactory, Environment environment,
            IEntityRepository entityRepository, IIdGenerator idGenerator)
        {
            _currentPlayerService = currentPlayerService;
            _signalBus = signalBus;
            _cameraFollowService = cameraFollowService;
            _bindableViewFactory = bindableViewFactory;
            _environment = environment;
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
        }

        public CharacterSignalController Create()
        {
            PeasantFactory peasantFactory = new PeasantFactory();

            CanPushInventoryQuery canPushInventoryQuery = new CanPushInventoryQuery(_entityRepository);

            IngredientInteractorViewModelComponentFactory ingredientInteractorViewModelComponentFactory =
                new IngredientInteractorViewModelComponentFactory(
                    canPushInventoryQuery
                );

            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory =
                new VisibilityViewModelComponentFactory(_entityRepository);

            AnimationSpeedViewModelComponentFactory animationSpeedViewModelComponentFactory =
                new AnimationSpeedViewModelComponentFactory(_entityRepository);

            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory =
                new LookDirectionViewModelComponentFactory(_entityRepository);

            CharacterControllerMovementViewModelComponentFactory characterControllerMovementViewModelComponentFactory =
                new CharacterControllerMovementViewModelComponentFactory(_entityRepository);

            CharacterViewModelFactory characterViewModelFactory = new CharacterViewModelFactory(
                visibilityViewModelComponentFactory,
                animationSpeedViewModelComponentFactory,
                lookDirectionViewModelComponentFactory,
                characterControllerMovementViewModelComponentFactory,
                ingredientInteractorViewModelComponentFactory
            );

            BindableViewBuilder<CharacterViewModel> characterViewBuilder = new BindableViewBuilder<CharacterViewModel>(
                _bindableViewFactory,
                characterViewModelFactory,
                _environment.View["Character"]
            );

            CreateCurrentCharacterQuery createCurrentCharacterQuery = new CreateCurrentCharacterQuery(
                _entityRepository,
                peasantFactory,
                _idGenerator
            );

            CreateCharacterSignalAction createCharacterSignalAction =
                new CreateCharacterSignalAction(
                    _signalBus,
                    characterViewBuilder,
                    _currentPlayerService,
                    _cameraFollowService,
                    createCurrentCharacterQuery
                );

            MovePositionCommand movePositionCommand = new MovePositionCommand(_entityRepository);
            SetLookDirectionCommand setLookDirectionCommand = new SetLookDirectionCommand(_entityRepository);
            SetSpeedCommand setSpeedCommand = new SetSpeedCommand(_entityRepository);

            CharacterMoveSignalAction characterMoveSignalAction =
                new CharacterMoveSignalAction(_currentPlayerService, movePositionCommand);
            CharacterRotateSignalAction characterRotateSignalAction =
                new CharacterRotateSignalAction(_currentPlayerService, setLookDirectionCommand);
            CharacterSpeedSignalAction characterSpeedSignalAction =
                new CharacterSpeedSignalAction(_currentPlayerService, setSpeedCommand);

            return new CharacterSignalController
            (
                new ISignalAction[]
                {
                    createCharacterSignalAction,
                    characterMoveSignalAction,
                    characterRotateSignalAction,
                    characterSpeedSignalAction,
                }
            );
        }
    }
}