using Sources.Client.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Factories.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.Characters.Queries
{
    public class CreateCurrentCharacterQuery
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IPeasantFactory _peasantFactory;
        private readonly IIdGenerator _idGenerator;

        public CreateCurrentCharacterQuery
        (
            IEntityRepository entityRepository,
            IPeasantFactory peasantFactory,
            IIdGenerator idGenerator
        )
        {
            _entityRepository = entityRepository;
            _peasantFactory = peasantFactory;
            _idGenerator = idGenerator;
        }

        public int Handle(Vector3 spawnPosition)
        {
            PeasantSpawnInfo spawnInfo = new PeasantSpawnInfo(spawnPosition);
            int id = _idGenerator.GetId();

            Character character = _peasantFactory.Create(id, spawnInfo);
            _entityRepository.Add(character);

            return id;
        }
    }
}