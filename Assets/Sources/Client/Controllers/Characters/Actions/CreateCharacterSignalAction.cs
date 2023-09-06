using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Characters.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CreateCharacterSignalAction : ISignalAction<CreateCharacterSignal>
    {
        private readonly ICharacterFactory _characterFactory;
        private readonly IEntityRepository _entityRepository;
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly CurrentPlayerService _currentPlayerService;
        private readonly CameraFollowService _cameraFollowService;
        private readonly IIdGenerator _idGenerator;
        private readonly CharacterViewModelFactory _characterViewModelFactory;
        private readonly CreateCurrentCharacterQuery _createCurrentCharacterQuery;

        public CreateCharacterSignalAction
        (
            ICharacterFactory characterFactory,
            IEntityRepository entityRepository,
            IBindableViewFactory bindableViewFactory,
            CurrentPlayerService currentPlayerService,
            CameraFollowService cameraFollowService,
            IIdGenerator idGenerator,
            CharacterViewModelFactory characterViewModelFactory,
            CreateCurrentCharacterQuery createCurrentCharacterQuery
        )
        {
            _characterFactory = characterFactory;
            _entityRepository = entityRepository;
            _bindableViewFactory = bindableViewFactory;
            _currentPlayerService = currentPlayerService;
            _cameraFollowService = cameraFollowService;
            _idGenerator = idGenerator;
            _characterViewModelFactory = characterViewModelFactory;
            _createCurrentCharacterQuery = createCurrentCharacterQuery;
        }

        public void Handle(CreateCharacterSignal signal)
        {
            int id = _createCurrentCharacterQuery.Handle(signal.SpawnPosition);
            
            IViewModel viewModel = _characterViewModelFactory.Create(id);
            IBindableView view = _bindableViewFactory.Create("", "Peasant"); //todo: Make constant path

            _currentPlayerService.CharacterId = id;
            _cameraFollowService.Follow(((MonoBehaviour)view).transform);

            view.Bind(viewModel);
        }
    }
}