using System;
using Presentation.Frameworks.Mvvm.Factories;
using Sources.Client.Presentation.Views.Ingredients;

namespace Sources.Client.Infrastructure.Factories.Presentation.Views
{
    public class IngredientViewFactory
    {
        private readonly PrefabFactory _prefabFactory;
        private static readonly string s_ingredientPrefabPath = "Views/Ingredients/";

        public IngredientViewFactory(PrefabFactory prefabFactory) =>
            _prefabFactory = prefabFactory;

        public IngredientView Create(Type type) =>
            _prefabFactory.Create<IngredientView>(s_ingredientPrefabPath + type.Name + "View");
    }
}