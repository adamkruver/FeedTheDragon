using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class GetInventoryIdQuery : ComponentUseCaseBase<Inventory>
    {
        public GetInventoryIdQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }
        
        public int Handle(int playerId) => 
            GetComponent(playerId).Id;
    }
}