using System.Collections.Generic;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.InventoryComponents.Queries
{
    public class GetInvenoryItemTypesQuery : ComponentUseCaseBase<InventoryComponent>
    {
        public GetInvenoryItemTypesQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public IEnumerable<IIngredientType> Handle(int id) =>
            GetComponent(id).IngredientTypes;
    }
}