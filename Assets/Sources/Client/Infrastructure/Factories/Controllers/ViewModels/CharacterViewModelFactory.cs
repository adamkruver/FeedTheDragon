using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Characters.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Domain.Characters;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class CharacterViewModelFactory
    {
        public IViewModel Create(Character character) =>
            new CharacterViewModel(new IViewModelComponent[]
            {
                new CharacterControllerMoveViewModelComponent(character),
                new LookDirectionViewModelComponent(character),
                new VisibilityViewModelComponent(character),
                new AnimationSpeedViewModelComponent(character),
            });
    }
}