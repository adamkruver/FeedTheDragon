using Sources.Client.Domain.Enemies;
using Sources.Client.Domain.NPCs.Bears;
using Sources.Client.Infrastructure.Factories.Domain.Enemies;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.Enemies.Bears
{
    public class CreateBearQuery
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly BearFactory _spiderFactory = new BearFactory();

        public CreateBearQuery(
            IEntityRepository entityRepository,
            IIdGenerator idGenerator
        )
        {
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
        }

        public int Handle(Vector3 position)
        {
            int id = _idGenerator.GetId();

            EnemySpawnInfo spawnInfo = new EnemySpawnInfo(position);
            Enemy bear = _spiderFactory.Create(id, new Bear(), spawnInfo);
            _entityRepository.Add(bear);

            return id;
        }
    }
}