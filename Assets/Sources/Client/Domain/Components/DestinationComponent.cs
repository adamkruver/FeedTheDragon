using System;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Domain.Components
{
    public class DestinationComponent : IComponent, IDisposable
    {
        private static readonly float MinEpsilon = 0.1f; // todo: to constants
        
        private readonly PositionComponent _positionComponent;
        private readonly SpeedComponent _speedComponent;

        private MutableLiveData<Vector3> _destination;
        private MutableLiveData<bool> _isReached = new MutableLiveData<bool>();

        public DestinationComponent(Vector3 destination, PositionComponent positionComponent,
            SpeedComponent speedComponent)
        {
            _positionComponent = positionComponent;
            _speedComponent = speedComponent;
            _destination = new MutableLiveData<Vector3>(destination);

            _positionComponent.Position.Observe(OnPositionChanged);
        }

        public Vector3 MoveDirection =>
            _speedComponent.Speed.Value * (_destination.Value - _positionComponent.Position.Value).normalized;

        public LiveData<bool> IsReached => _isReached;

        public void Set(Vector3 destination)
        {
            _destination.Value = destination;
            _isReached.Value = false;
        }

        private void OnPositionChanged(Vector3 currentPosition)
        {
            _isReached.Value = Vector3.Distance(currentPosition, _destination.Value) < MinEpsilon;
        }

        public void Dispose()
        {
            _positionComponent.Position.Unobserve(OnPositionChanged);
            _destination?.Dispose();
            _isReached?.Dispose();
        }
    }
}