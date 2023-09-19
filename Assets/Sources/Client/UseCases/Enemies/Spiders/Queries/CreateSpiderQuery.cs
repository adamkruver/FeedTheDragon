using Sources.Client.Domain.Enemies;
using Sources.Client.Domain.Enemies.Types;
using Sources.Client.Infrastructure.Factories.Domain.Enemies;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.Enemies.Spiders.Queries
{
    public class CreateSpiderQuery
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly SpiderFactory _spiderFactory = new SpiderFactory();

        public CreateSpiderQuery(
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
            Enemy spider = _spiderFactory.Create(id, new Spider(), spawnInfo);
            _entityRepository.Add(spider);

            return id;
        }
    }
}