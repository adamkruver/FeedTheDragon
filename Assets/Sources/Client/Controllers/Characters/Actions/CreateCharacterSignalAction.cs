using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Services.CameraFollowService;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Presentation.Views;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Characters.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CreateCharacterSignalAction : ISignalAction<CreateCharacterSignal>
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly CurrentPlayerService _currentPlayerService;
        private readonly CameraFollowService _cameraFollowService;
        private readonly CharacterViewModelFactory _characterViewModelFactory;
        private readonly CreateCurrentCharacterQuery _createCurrentCharacterQuery;
        private readonly InventoryViewModelFactory _inventoryViewModelFactory;
        private readonly IInventoryViewFactory _inventoryViewFactory;

        public CreateCharacterSignalAction
        (
            IBindableViewFactory bindableViewFactory,
            CurrentPlayerService currentPlayerService,
            CameraFollowService cameraFollowService,
            CharacterViewModelFactory characterViewModelFactory,
            CreateCurrentCharacterQuery createCurrentCharacterQuery,
            InventoryViewModelFactory inventoryViewModelFactory,
            IInventoryViewFactory inventoryViewFactory
        )
        {
            _bindableViewFactory = bindableViewFactory;
            _currentPlayerService = currentPlayerService;
            _cameraFollowService = cameraFollowService;
            _characterViewModelFactory = characterViewModelFactory;
            _createCurrentCharacterQuery = createCurrentCharacterQuery;
            _inventoryViewModelFactory = inventoryViewModelFactory;
            _inventoryViewFactory = inventoryViewFactory;
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