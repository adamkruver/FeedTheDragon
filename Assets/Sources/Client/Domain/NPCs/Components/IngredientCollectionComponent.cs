using System.Linq;
using Sources.Client.Domain.Ingredients;

namespace Sources.Client.Domain.NPCs.Components
{
    public class IngredientCollectionComponent : IComponent
    {
        private IIngredientType[] _types;
        
        public IngredientCollectionComponent(IIngredientType[] types)
        {
            _types = types;
        }

        public IIngredientType[] Types => _types.ToArray();
    }
}