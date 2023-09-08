using System.IO;
using Sources.Client.Domain.Ingredients;
using Sources.Client.Infrastructure.Repositories;
using Sources.Client.InfrastructureInterfaces.Data;
using Sources.Client.Presentation.Views.Ingredients;
using UnityEngine;

namespace Sources.Client.Infrastructure.Data.Providers
{
    public class IngredientViewProvider : IDataProvider<IIngredientType, IngredientView>
    {
        private readonly AssetRepository<IngredientView> _repository = new AssetRepository<IngredientView>();
        private readonly string _ingredientIconPrefabPath = @"Views/Ingredients/";

        public IngredientView Load<T>() where T: IIngredientType
        {
            string fullPath = Path.Combine(_ingredientIconPrefabPath, typeof(T).Name + "View");

            IngredientView view = _repository.Get(fullPath);

            if (view == null)
            {
                view = Resources.Load<IngredientView>(fullPath);
                _repository.Set(fullPath, view);
            }

            return view;
        }
    }
}