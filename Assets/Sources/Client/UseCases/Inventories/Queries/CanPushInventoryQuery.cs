using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class CanPushInventoryQuery : ComponentUseCaseBase<Inventory>
    {
        public CanPushInventoryQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public bool Handle(int id) =>
            GetComponent(id).CanPush;
    }
}