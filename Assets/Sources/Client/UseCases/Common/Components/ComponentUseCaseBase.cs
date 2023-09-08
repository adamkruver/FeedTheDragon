using System;
using Sources.Client.Domain;
using Sources.Client.InfrastructureInterfaces.Repositories;

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
            if (_entityRepository.Get(id) is not Composite composite)
                throw new NullReferenceException();

            if (composite.TryGetComponent(out T component) == false)
                throw new NullReferenceException();

            return component;
        }
    }
}