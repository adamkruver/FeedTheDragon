using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Client.Domain.Entities;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.Infrastructure.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private List<Enity> _entities = new List<Enity>();

        public void Add(Enity entity)
        {
            if (_entities.Contains(entity))
                throw new AggregateException();
            
            _entities.Add(entity);
        }

        public Enity Get(int id)
        {
            return _entities.FirstOrDefault(entity => entity.Id == id) ?? throw new NullReferenceException();
        }
    }
}