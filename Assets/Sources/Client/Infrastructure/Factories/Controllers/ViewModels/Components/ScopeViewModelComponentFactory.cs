using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.UseCases.Common.Components.FirstContacts.Commands;
using Sources.Client.UseCases.Entities.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class ScopeViewModelComponentFactory
    {
        private readonly AddKnownTypeCommand _addKnownTypeCommand;
        private readonly GetEntityTypeQuery _getEntityTypeQuery;

        public ScopeViewModelComponentFactory(IEntityRepository entityRepository)
        {
            _addKnownTypeCommand = new AddKnownTypeCommand(entityRepository);
            _getEntityTypeQuery = new GetEntityTypeQuery(entityRepository);
        }

        public ScopeViewModelComponent Create(int id) =>
            new ScopeViewModelComponent(
                id,
                _addKnownTypeCommand,
                _getEntityTypeQuery
            );
    }
}