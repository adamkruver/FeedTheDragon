using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Domain.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels
{
    public class IngredientViewModelFactory
    {
        public IViewModel Create(Ingredient ingredient) =>
            new IngredientViewModel(new IViewModelComponent[]
                {
                    new VisibilityViewModelComponent(ingredient),
                    new PositionViewModelComponent(ingredient),
                },
                ingredient
            );
    }
}