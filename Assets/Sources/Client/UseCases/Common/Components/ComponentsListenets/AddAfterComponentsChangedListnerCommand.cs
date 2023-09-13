using System;
using Sources.Client.Domain;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.ComponentsListenets
{
    public class AddAfterComponentsChangedListnerCommand
    {
        private readonly IEntityRepository _entityRepository;

        public AddAfterComponentsChangedListnerCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Hanlde(int compositeId, Action callback)
        {
            if (_entityRepository.Get(compositeId) is not Composite composite)
                throw new InvalidCastException();
            
            composite.AfterComponentsChanged += callback;
        }
    }
}