using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Controllers.Inventories.Signals;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels;
using Sources.Client.Infrastructure.Services.Cameras;
using Sources.Client.Infrastructure.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.Builders.Presentation.BindableViews;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.InfrastructureInterfaces.SignalBus.Actions.Generic;
using Sources.Client.UseCases.Characters.Queries;
using UnityEngine;

namespace Sources.Client.Controllers.Characters.Actions
{
    public class CreateCharacterSignalAction : ISignalAction<CreateCharacterSignal>
    {
        private readonly ISignalBus _signalBus;
        private readonly IBindableViewBuilder<CharacterViewModel> _viewBuilder;
        private readonly CurrentPlayerService _currentPlayerService;
        private readonly CameraFollowService _cameraFollowService;
        private readonly CreateCurrentCharacterQuery _createCurrentCharacterQuery;

        public CreateCharacterSignalAction
        (
            ISignalBus signalBus,
            IBindableViewBuilder<CharacterViewModel> viewBuilder,
            CurrentPlayerService currentPlayerService,
            CameraFollowService cameraFollowService,
            CreateCurrentCharacterQuery createCurrentCharacterQuery
        )
        {
            _signalBus = signalBus;
            _viewBuilder = viewBuilder;
            _currentPlayerService = currentPlayerService;
            _cameraFollowService = cameraFollowService;
            _createCurrentCharacterQuery = createCurrentCharacterQuery;
        }

        public void Handle(CreateCharacterSignal signal)
        {
            int characterId = _createCurrentCharacterQuery.Handle(signal.SpawnPosition);
            _signalBus.Handle(new CreateInventorySignal(characterId, 3)); //todo: to config

            IBindableView view = _viewBuilder.Build(characterId, "Peasant");
            
            _currentPlayerService.CharacterId = characterId;
            _cameraFollowService.Follow(((MonoBehaviour)view).transform);

        }
    }
}