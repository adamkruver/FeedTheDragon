using System;
using Sources.Client.Domain;
using Sources.Client.Domain.Entities;
using Sources.Client.InfrastructureInterfaces.Repositories;
using UnityEngine;

namespace Sources.Client.UseCases.Common.Components
{
    public abstract class ComponentUseCaseBase<T> where T : IComponent
    {
        private readonly IEntityRepository _entityRepository;

        protected ComponentUseCaseBase(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        protected T GetComponent(int id)
        {
            IEntity entity = _entityRepository.Get(id);
            
            if (entity is not Composite composite)
                throw new NullReferenceException();

            if (composite.TryGetComponent(out T component) == false)
                throw new NullReferenceException();

            return component;
        }
    }
}