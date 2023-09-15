using System;
using Presentation.Frameworks.Mvvm.Factories;
using PresentationInterfaces.Frameworks.Mvvm.Factories;
using Sources.Client.Presentation.Views.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Presentation.Views
{
    public class IngredientViewFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private static readonly string s_ingredientPrefabPath = "UIs/Ingredients/";

        public IngredientViewFactory(IPrefabFactory prefabFactory) =>
            _prefabFactory = prefabFactory;

        public IngredientView Create(Type type) =>
            _prefabFactory.Create<IngredientView>(s_ingredientPrefabPath + type.Name + "View");
    }
}