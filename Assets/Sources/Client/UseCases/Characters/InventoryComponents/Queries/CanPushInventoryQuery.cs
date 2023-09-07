using Sources.Client.Domain.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Characters.InventoryComponents.Queries
{
    public class CanPushInventoryQuery : ComponentUseCaseBase<InventoryComponent>
    {
        public CanPushInventoryQuery(IEntityRepository entityRepository) : base(entityRepository)
        {
        }

        public bool Handle(int id) =>
            GetComponent(id).CanPush;
    }
}