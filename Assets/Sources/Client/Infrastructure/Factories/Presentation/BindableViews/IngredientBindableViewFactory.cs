using PresentationInterfaces.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Views;
using Sources.Client.App.Configs;
using Sources.Client.Domain.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Presentation.BindableViews
{
    public class IngredientBindableViewFactory
    {
        private readonly IBindableViewFactory _bindableViewFactory;
        private readonly Environment _environment;

        public IngredientBindableViewFactory(IBindableViewFactory bindableViewFactory, Environment environment)
        {
            _bindableViewFactory = bindableViewFactory;
            _environment = environment;
        }

        public IBindableView Create(IIngredientType type)
        {
            string typeName = type.GetType().Name;
            string prefabPath = _environment.View["Ingredient"];

            return _bindableViewFactory.Create(prefabPath, typeName);
        }
    }
}