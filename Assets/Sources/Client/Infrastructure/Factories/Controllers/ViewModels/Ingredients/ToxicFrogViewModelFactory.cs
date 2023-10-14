using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients.Components;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients
{
    public class ToxicFrogViewModelFactory : IngredientViewModelFactoryBase
    {
        private readonly MoveToDestinationViewModelComponentFactory _moveToDestinationViewModelComponentFactory;
        private readonly ToxicFrogBehaviourTreeViewModelComponentFactory _toxicFrogBehaviourTreeViewModelComponentFactory;
        private readonly LookDirectionViewModelComponentFactory _lookDirectionViewModelComponentFactory;

        public ToxicFrogViewModelFactory
        (
            VisibilityViewModelComponentFactory visibilityViewModelComponentFactory,
            PositionViewModelComponentFactory positionViewModelComponentFactory,
            IngredientClickViewModelComponentFactory ingredientClickViewModelComponentFactory,
            MoveToDestinationViewModelComponentFactory moveToDestinationViewModelComponentFactory,
            ToxicFrogBehaviourTreeViewModelComponentFactory toxicFrogBehaviourTreeViewModelComponentFactory,
            LookDirectionViewModelComponentFactory lookDirectionViewModelComponentFactory
        ) :
            base(visibilityViewModelComponentFactory, positionViewModelComponentFactory,
                ingredientClickViewModelComponentFactory)
        {
            _moveToDestinationViewModelComponentFactory = moveToDestinationViewModelComponentFactory;
            _toxicFrogBehaviourTreeViewModelComponentFactory = toxicFrogBehaviourTreeViewModelComponentFactory;
            _lookDirectionViewModelComponentFactory = lookDirectionViewModelComponentFactory;
        }

        protected override IViewModelComponent[] CreateComponents(int id) =>
            new IViewModelComponent[]
            {
                _moveToDestinationViewModelComponentFactory.Create(id),
                _toxicFrogBehaviourTreeViewModelComponentFactory.Create(id),
                _lookDirectionViewModelComponentFactory.Create(id),
            };
    }
}