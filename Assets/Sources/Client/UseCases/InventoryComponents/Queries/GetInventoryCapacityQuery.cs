using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.InventoryComponents.Queries
{
    public class GetInventoryCapacityQuery : ComponentUseCaseBase<InventoryComponent>
    {
        public GetInventoryCapacityQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public int Handle(int id) => 
            GetComponent(id).MaxCount;
    }
}