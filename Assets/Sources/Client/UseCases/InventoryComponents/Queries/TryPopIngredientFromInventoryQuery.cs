using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.InventoryComponents.Queries
{
    public class TryPopIngredientFromInventoryQuery : ComponentUseCaseBase<InventoryComponent>
    {
        public TryPopIngredientFromInventoryQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public bool Handle(int id, out Ingredient ingredient) => 
            GetComponent(id).TryPop(out ingredient);
    }
}