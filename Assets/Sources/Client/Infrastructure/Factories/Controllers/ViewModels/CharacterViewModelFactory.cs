using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class CharacterViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly AnimationSpeedViewModelComponentFactory _animationSpeedViewModelComponentFactory;
        private readonly LookDirectionViewModelComponentFactory _lookDirectionViewModelComponentFactory;

        private readonly CharacterControllerMovementViewModelComponentFactory
            _characterControllerMovementViewModelComponentFactory;

        public CharacterViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            AnimationSpeedViewModelComponentFactory animationSpeedViewModelComponentFactory,
            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory,
            CharacterControllerMovementViewModelComponentFactory characterControllerMovementViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _animationSpeedViewModelComponentFactory = animationSpeedViewModelComponentFactory;
            _lookDirectionViewModelComponentFactory = lookDirectionViewModelComponentFactory;
            _characterControllerMovementViewModelComponentFactory =
                characterControllerMovementViewModelComponentFactory;
        }

        public IViewModel Create(int id) =>
            new CharacterViewModel(new IViewModelComponent[]
            {
                _characterControllerMovementViewModelComponentFactory.Create(id),
                _lookDirectionViewModelComponentFactory.Create(id),
                _visibilityViewModelComponentFactory.Create(id),
                _animationSpeedViewModelComponentFactory.Create(id)
            });
    }
}