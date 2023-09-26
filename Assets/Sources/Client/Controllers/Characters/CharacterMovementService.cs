using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Controllers.Characters
{
    public class CharacterMovementService : IUpdatable
    {
        private readonly ISignalBus _signalBus;
        private readonly LiveData<Vector3> _position;
        private readonly LiveData<float> _speed;
        
        private readonly float _speedDelta = 5f;

        private Vector3 _direction;

        private float _currentSpeed;
        private bool _isMoving;
        private Vector3 _destination;

        public CharacterMovementService(
            ISignalBus signalBus,
            ICurrentPlayerService currentPlayerService,
            GetPositionQuery getPositionQuery,
            GetSpeedQuery getSpeedQuery
        )
        {
            _signalBus = signalBus;
            _position = getPositionQuery.Handle(currentPlayerService.CharacterId);
            _speed = getSpeedQuery.Handle(currentPlayerService.CharacterId);
        }

        public void MoveTo(Vector3 point)
        {
            _destination = point;
            _isMoving = true;
        }

        public void Stop()
        {
            _isMoving = false;
        }

        private void SetSpeed(float speed)
        {
            _currentSpeed = Mathf.Clamp(speed, 0, 1);
            _signalBus.Handle(new CharacterSpeedSignal(_currentSpeed));
        }

        public void Update(float deltaTime)
        {
            if (_isMoving)
            {
                _direction = _destination - _position.Value;
                _direction.y = 0;

                Vector3 moveDelta = _speed.Value * Time.deltaTime * _direction.normalized;

                _signalBus.Handle(new CharacterRotateSignal(moveDelta));
                _signalBus.Handle(new CharacterMoveSignal(moveDelta));
            
                SetSpeed(_currentSpeed + _speedDelta * Time.deltaTime);
            }
            else
            {
                SetSpeed(_currentSpeed - _speedDelta * Time.deltaTime);
            }
        }
    }
}