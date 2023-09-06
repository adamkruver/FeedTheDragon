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
        private readonly ICharacterFactory _characterFactory;
        private readonly IIdGenerator _idGenerator;

        public CreateCurrentCharacterQuery
        (
            IEntityRepository entityRepository,
            ICharacterFactory characterFactory,
            IIdGenerator idGenerator
        )
        {
            _entityRepository = entityRepository;
            _characterFactory = characterFactory;
            _idGenerator = idGenerator;
        }

        public int Handle(Vector3 spawnPosition)
        {
            CharacterSpawnInfo spawnInfo = new CharacterSpawnInfo(spawnPosition);
            int id = _idGenerator.GetId();

            Character character = _characterFactory.Create(id, spawnInfo);
            _entityRepository.Add(character);
            
            return id;
        }
    }
}