using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class IngredientViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;

        public IngredientViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new IngredientViewModel(new[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                    _positionViewModelComponentFactory.Create(id)
                }
            );
        }
    }
}