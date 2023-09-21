using Sources.Client.Domain.NPCs.Dragons;
using Sources.Client.Infrastructure.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.NPCs.Dragons
{
    public class CreateDragonQuery
    {
        private readonly DragonFactory _dragonFactory = new DragonFactory(); 
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;

        public CreateDragonQuery(IEntityRepository entityRepository, IIdGenerator idGenerator)
        {
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
        }

        public int Handle(Vector3 position)
        {
            int id = _idGenerator.GetId();
            
            Dragon dragon = _dragonFactory.Create(id, new DragonSpawnInfo(position));
            _entityRepository.Add(dragon);
            
            return id;
        }
    }
}