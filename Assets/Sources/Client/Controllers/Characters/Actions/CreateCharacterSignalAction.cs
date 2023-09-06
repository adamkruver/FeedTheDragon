using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Domain.Characters;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
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

        public CreateCharacterSignalAction
        (
            ICharacterFactory characterFactory,
            IEntityRepository entityRepository,
            IBindableViewFactory bindableViewFactory,
            CurrentPlayerService currentPlayerService,
            CameraFollowService cameraFollowService,
            IIdGenerator idGenerator
        )
        {
            _characterFactory = characterFactory;
            _entityRepository = entityRepository;
            _bindableViewFactory = bindableViewFactory;
            _currentPlayerService = currentPlayerService;
            _cameraFollowService = cameraFollowService;
            _idGenerator = idGenerator;
        }

        public void Handle(CreateCharacterSignal signal)
        {
            CharacterSpawnInfo spawnInfo = new CharacterSpawnInfo(signal.SpawnPosition);
            
            Character character = _characterFactory.Create(_idGenerator.GetId(), spawnInfo);
            _entityRepository.Add(character);

            CharacterViewModel viewModel = new CharacterViewModel(new IViewModelComponent[] { }, character);
            IBindableView view = _bindableViewFactory.Create("", "Peasant"); //todo: Make constant path

            _currentPlayerService.Character = character;

            _cameraFollowService.Follow(((MonoBehaviour)view).transform);

            view.Bind(viewModel);
        }
    }
}