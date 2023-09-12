using Sources.Client.Domain.NPCs;
using Sources.Client.Domain.NPCs.Ogres;
using Sources.Client.Infrastructure.Factories.Domain.NPCs;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.IdGenerators;
using UnityEngine;

namespace Sources.Client.UseCases.NPCs.Ogres.Queries
{
    public class CreateOgreQuery
    {
        private readonly OgreFactory _ogreFactory = new OgreFactory(); 
        private readonly IEntityRepository _entityRepository;
        private readonly IIdGenerator _idGenerator;

        public CreateOgreQuery(IEntityRepository entityRepository, IIdGenerator idGenerator)
        {
            _entityRepository = entityRepository;
            _idGenerator = idGenerator;
        }

        public int Handle(Vector3 position)
        {
            int id = _idGenerator.GetId();
            
            Ogre ogre = _ogreFactory.Create(id, new OgreSpawnInfo(position));
            _entityRepository.Add(ogre);
            
            return id;
        }
    }
}