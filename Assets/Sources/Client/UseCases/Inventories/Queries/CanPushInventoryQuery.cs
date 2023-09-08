using Sources.Client.Domain.Inventories;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components;

namespace Sources.Client.UseCases.Inventories.Queries
{
    public class CanPushInventoryQuery : ComponentUseCaseBase<Inventory>
    {
        private readonly IEntityRepository _repository;

        public CanPushInventoryQuery(IEntityRepository repository) : base(repository) =>
            _repository = repository;

        public bool Handle(int id) =>
            GetComponent(id).CanPush;
    }
}