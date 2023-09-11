using PresentationInterfaces.Frameworks.Mvvm.ViewModels;
using Sources.Client.InfrastructureInterfaces.Services.GameUpdateService;
using Sources.Client.UseCases.Common.Components.Destinations.Queries;
using Sources.Client.UseCases.Common.Components.Positions.Commands;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.ViewModels.Components
{
    public class MoveToDestinationViewModelComponent : IViewModelComponent
    {
        private readonly int _id;
        private readonly IUpdateService _updateService;
        private readonly MovePositionCommand _movePositionCommand;
        private readonly GetMoveImpulseQuery _getMoveImpulseQuery;

        private LiveData<bool> _isReached;

        public MoveToDestinationViewModelComponent
        (
            int id,
            IUpdateService updateService,
            MovePositionCommand movePositionCommand,
            GetDestinationReachedQuery getDestinationReachedQuery,
            GetMoveImpulseQuery getMoveImpulseQuery
        )
        {
            _id = id;
            _updateService = updateService;
            _movePositionCommand = movePositionCommand;
            _getMoveImpulseQuery = getMoveImpulseQuery;
            _isReached = getDestinationReachedQuery.Handle(id);
        }

        public void Enable()
        {
            _updateService.Updating += OnUpdate;
        }

        public void Disable()
        {
            _updateService.Updating -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            if (_isReached.Value)
                return;
            
            _movePositionCommand.Handle(_id, _getMoveImpulseQuery.Handle(_id) * deltaTime);
        }
    }
}