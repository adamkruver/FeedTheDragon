using Sources.Client.Domain.Entities;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Entities.Queries
{
    public class GetEntityTypeQuery
    {
        private readonly IEntityRepository _entityRepository;

        public GetEntityTypeQuery(IEntityRepository entityRepository) => 
            _entityRepository = entityRepository;

        public IEntityType Handle(int id) =>
            _entityRepository.Get(id).EntityType;
    }
}