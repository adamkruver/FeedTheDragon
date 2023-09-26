using Sources.Client.Controllers.Characters;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;

namespace Sources.Client.Infrastructure.Factories.Controllers
{
    public class CharacterMovementServiceFactory
    {
        private readonly ISignalBus _signalBus;
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly GetPositionQuery _getPositionQuery;
        private readonly GetSpeedQuery _getSpeedQuery;

        public CharacterMovementServiceFactory(
            ISignalBus signalBus,
            ICurrentPlayerService currentPlayerService,
            IEntityRepository entityRepository
        )
        {
            _signalBus = signalBus;
            _currentPlayerService = currentPlayerService;
            _getPositionQuery = new GetPositionQuery(entityRepository);
            _getSpeedQuery = new GetSpeedQuery(entityRepository);
        }

        public CharacterMovementService Create() =>
            new CharacterMovementService(
                _signalBus,
                _currentPlayerService,
                _getPositionQuery,
                _getSpeedQuery
            );
    }
}