using Sources.Client.Controllers.Ingredients.ViewModels.Components;
using Sources.Client.InfrastructureInterfaces.Repositories;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.Common.Components.AnimationSpeeds.Queries;
using Sources.Client.UseCases.Common.Components.Destinations.Queries;
using Sources.Client.UseCases.Common.Components.LookDirection.Commands;
using Sources.Client.UseCases.Common.Components.LookDirection.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Commands;

namespace Sources.Client.Infrastructure.Factories.Controllers.ViewModels.Ingredients.Components
{
    public class ToxicFrogBehaviourTreeViewModelComponentFactory
    {
        private readonly ISignalBus _signalBus;
        private readonly GetDestinationReachedQuery _getDestinationReachedQuery;
        private readonly SetLookDirectionCommand _setLookDirectionCommand;
        private readonly GetLookDirectionQuery _getLookDirectionQuery;
        private readonly GetSpeedQuery _getSpeedQuery;
        private readonly SetSpeedCommand _setSpeedCommand;

        public ToxicFrogBehaviourTreeViewModelComponentFactory(
            ISignalBus signalBus,
            IEntityRepository entityRepository
        )
        {
            _signalBus = signalBus;
            _setLookDirectionCommand = new SetLookDirectionCommand(entityRepository);
            _getLookDirectionQuery = new GetLookDirectionQuery(entityRepository);
            _getDestinationReachedQuery = new GetDestinationReachedQuery(entityRepository);
            _getSpeedQuery = new GetSpeedQuery(entityRepository);
        }

        public ToxicFrogBehaviourTreeViewModelComponent Create(int id) =>
            new ToxicFrogBehaviourTreeViewModelComponent(
                id,
                _signalBus,
                _setLookDirectionCommand,
                _getLookDirectionQuery,
                _getSpeedQuery,
                _getDestinationReachedQuery
            );
    }
}