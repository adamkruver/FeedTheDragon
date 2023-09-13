using System;
using Sources.Client.Domain;
using Sources.Client.InfrastructureInterfaces.Repositories;

namespace Sources.Client.UseCases.Common.Components.ComponentsListenets
{
    public class RemoveAfterComponentsChangedListnerCommand
    {
        private readonly IEntityRepository _entityRepository;

        public RemoveAfterComponentsChangedListnerCommand(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Hanlde(int compositeId, Action callback)
        {
            if (_entityRepository.Get(compositeId) is not Composite composite)
                throw new InvalidCastException();
            
            composite.AfterComponentsChanged -= callback;
        }
    }
}