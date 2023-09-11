using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients
{
    public class ToxicFrogViewModelFactory : IngredientViewModelFactoryBase
    {
        private readonly MoveToDestinationViewModelComponentFactory _moveToDestinationViewModelComponentFactory;

        public ToxicFrogViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            IngredientClickViewModelComponentFactory ingredientClickViewModelComponentFactory,
            MoveToDestinationViewModelComponentFactory moveToDestinationViewModelComponentFactory
        ) :
            base(visibilityViewModelComponentFactory, positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory)
        {
            _moveToDestinationViewModelComponentFactory = moveToDestinationViewModelComponentFactory;
        }

        protected override IViewModelComponent[] CreateComponents(int id) =>
            new IViewModelComponent[]
            {
                _moveToDestinationViewModelComponentFactory.Create(id)
            };
    }
}