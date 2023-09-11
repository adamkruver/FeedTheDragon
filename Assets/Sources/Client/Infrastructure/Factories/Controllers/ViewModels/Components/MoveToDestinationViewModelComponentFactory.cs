using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.Controllers.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.GameUpdateService;
using Sources.Client.UseCases.Common.Components.Destinations.Queries;
using Sources.Client.UseCases.Common.Components.Positions.Commands;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Components
{
    public class MoveToDestinationViewModelComponentFactory
    {
        private readonly IUpdateService _updateService;
        private readonly GetDestinationReachedQuery _getDestinationReachedQuery;
        private readonly MovePositionCommand _movePositionCommand;
        private readonly GetMoveImpulseQuery _getMoveImpulseQuery;

        public MoveToDestinationViewModelComponentFactory(IUpdateService updateService, IEntityRepository entityRepository)
        {
            _updateService = updateService;
            
            _getMoveImpulseQuery = new GetMoveImpulseQuery(entityRepository);
            _movePositionCommand = new MovePositionCommand(entityRepository);
            _getDestinationReachedQuery = new GetDestinationReachedQuery(entityRepository);
        }
        
        public IViewModelComponent Create(int id)
        {
            return new MoveToDestinationViewModelComponent
                (id, _updateService, _movePositionCommand, _getDestinationReachedQuery, _getMoveImpulseQuery);
        }
    }
}