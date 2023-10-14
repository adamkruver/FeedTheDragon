using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Domain.Characters;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class CharacterViewModelFactory : IViewModelFactory<CharacterViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly AnimationSpeedViewModelComponentFactory _animationSpeedViewModelComponentFactory;
        private readonly LookDirectionViewModelComponentFactory _lookDirectionViewModelComponentFactory;
        private readonly IngredientInteractorViewModelComponentFactory _ingredientInteractorViewModelComponentFactory;
        private readonly FirstContactViewModelComponentFactory _firstContactViewModelComponentFactory;
        private readonly ScopeViewModelComponentFactory _scopeViewModelComponentFactory;

        private readonly CharacterControllerMovementViewModelComponentFactory
            _characterControllerMovementViewModelComponentFactory;

        public CharacterViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            AnimationSpeedViewModelComponentFactory animationSpeedViewModelComponentFactory,
            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory,
            CharacterControllerMovementViewModelComponentFactory characterControllerMovementViewModelComponentFactory,
            IngredientInteractorViewModelComponentFactory ingredientInteractorViewModelComponentFactory,
            FirstContactViewModelComponentFactory firstContactViewModelComponentFactory,
            ScopeViewModelComponentFactory scopeViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _animationSpeedViewModelComponentFactory = animationSpeedViewModelComponentFactory;
            _lookDirectionViewModelComponentFactory = lookDirectionViewModelComponentFactory;
            _characterControllerMovementViewModelComponentFactory =
                characterControllerMovementViewModelComponentFactory;
            _ingredientInteractorViewModelComponentFactory = ingredientInteractorViewModelComponentFactory;
            _firstContactViewModelComponentFactory = firstContactViewModelComponentFactory;
            _scopeViewModelComponentFactory = scopeViewModelComponentFactory;
        }

        public IViewModel Create(int characterId) =>
            new CharacterViewModel(
                new IViewModelComponent[]
            {
                _characterControllerMovementViewModelComponentFactory.Create(characterId),
                _lookDirectionViewModelComponentFactory.Create(characterId),
                _visibilityViewModelComponentFactory.Create(characterId),
                _animationSpeedViewModelComponentFactory.Create(characterId),
                _ingredientInteractorViewModelComponentFactory.Create(characterId),
                _firstContactViewModelComponentFactory.Create(characterId),
                _scopeViewModelComponentFactory.Create(characterId),
            });
    }
}