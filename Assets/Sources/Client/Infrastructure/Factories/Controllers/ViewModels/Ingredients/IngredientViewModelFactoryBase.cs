using System.Linq;
using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.Ingredients.ViewModels;
using Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers;
using Sources.Client.InfrastructureInterfaces.Factories.Controllers.ViewModels;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients
{
    public class IngredientViewModelFactoryBase : IViewModelFactory<IngredientViewModel>
    {
        private readonly VisibilityViewModelComponentFactory _visibilityViewModelComponentFactory;
        private readonly PositionViewModelComponentFactory _positionViewModelComponentFactory;
        private readonly IngredientClickViewModelComponentFactory _ingredientClickViewModelComponentFactory;

        public IngredientViewModelFactoryBase
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

        public IViewModel Create(int id) =>
            new IngredientViewModel(CreateAllComponents(id));

        protected virtual IViewModelComponent[] CreateComponents(int id) =>
            new IViewModelComponent[] { };

        private IViewModelComponent[] CreateBaseComponents(int id) =>
            new IViewModelComponent[]
            {
                _visibilityViewModelComponentFactory.Create(id),
                _positionViewModelComponentFactory.Create(id),
                _ingredientClickViewModelComponentFactory.Create(id)
            };

        private IViewModelComponent[] CreateAllComponents(int id) =>
            CreateBaseComponents(id)
                .Concat(CreateComponents(id))
                .ToArray();
    }
}