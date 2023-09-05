using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.CameraFollower;
using Sources.Client.Controllers.Characters.SIgnals;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Domain.Characters;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
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

        public CreateCharacterSignalAction
        (
            ICharacterFactory characterFactory,
            IEntityRepository entityRepository,
            IBindableViewFactory bindableViewFactory,
            CurrentPlayerService currentPlayerService,
            CameraFollowService cameraFollowService
        )
        {
            _characterFactory = characterFactory;
            _entityRepository = entityRepository;
            _bindableViewFactory = bindableViewFactory;
            _currentPlayerService = currentPlayerService;
            _cameraFollowService = cameraFollowService;
        }

        public void Handle(CreateCharacterSignal signal)
        {
            Character character = _characterFactory.Create(0, Vector3.zero); //todo Id generator
            _entityRepository.Add(character);

            CharacterViewModel viewModel = new CharacterViewModel(new IViewModelComponent[] { }, character);
            IBindableView view = _bindableViewFactory.Create("", "Peasant"); //todo: Make constant path

            _currentPlayerService.Character = character;

            _cameraFollowService.Follow(((MonoBehaviour)view).transform);

            view.Bind(viewModel);
        }
    }
}