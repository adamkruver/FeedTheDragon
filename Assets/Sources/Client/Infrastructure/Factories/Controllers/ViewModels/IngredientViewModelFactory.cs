using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class IngredientViewModelFactory
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly IngredientClickViewModelComponentFactory _ingredientClickViewModelComponentFactory;

        public IngredientViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            IngredientClickViewModelComponentFactory ingredientClickViewModelComponentFactory
        )
        {
            _visibilityViewModelComponentFactory = visibilityViewModelComponentFactory;
            _positionViewModelComponentFactory = positionViewModelComponentFactory;
            _ingredientClickViewModelComponentFactory = ingredientClickViewModelComponentFactory;
        }

        public IViewModel Create(int id)
        {
            return new IngredientViewModel(new[]
                {
                    _visibilityViewModelComponentFactory.Create(id),
                    _positionViewModelComponentFactory.Create(id),
                    _ingredientClickViewModelComponentFactory.Create(id)
                }
            );
        }
    }
}