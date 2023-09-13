using Sources.Client.Controllers.Characters.Signals;
using Sources.Client.Infrastructure.Services.Pointers;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using Sources.Client.UseCases.Common.Components.Positions.Queries;
using Sources.Client.UseCases.Common.Components.Speeds.Queries;
using UnityEngine;
using Utils.LiveDatas.Sources.Frameworks.LiveDatas;

namespace Sources.Client.Characters
{
    public class CharacterMovementService
    {
        private CharacterController _characterController;

        private readonly ISignalBus _signalBus;
        private readonly LiveData<Vector3> _position;
        private readonly LiveData<float> _animationSpeed;

        private readonly int _layer;
        private readonly PointerUIService _pointerUIService = new PointerUIService();

        private Camera _camera;
        private Vector3 _direction;

        private float _speed = 0;
        private float _speedDelta = 5f;

        public CharacterMovementService
        (
            ICurrentPlayerService currentPlayerService,
            ISignalBus signalBus,
            Camera camera,
            GetPositionQuery getPositionQuery, //todo: Убрать 
            GetSpeedQuery getSpeedQuery
        )
        {
            _camera = camera;
            _position = getPositionQuery.Handle(currentPlayerService.CharacterId);
            _animationSpeed = getSpeedQuery.Handle(currentPlayerService.CharacterId);
            _signalBus = signalBus;

            _layer = 1 << LayerMask.NameToLayer("Terrain"); //todo Move to constans
        }

        public void Update()
        {
            if (Input.GetMouseButton(0) == false)
            {
                _speed -= _speedDelta * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, 0, 1);
                _signalBus.Handle(new CharacterSpeedSignal(_speed));

                return;
            }

            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit,
                    Mathf.Infinity, _layer) == false || _pointerUIService.IsPointerOverUI)
            {
                _speed -= _speedDelta * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, 0, 1);
                _signalBus.Handle(new CharacterSpeedSignal(_speed));

                return;
            }

            _direction = raycastHit.point - _position.Value;
            _direction.y = 0;

            Vector3 moveDelta = _animationSpeed.Value * Time.deltaTime * _direction.normalized;

            _signalBus.Handle(new CharacterRotateSignal(moveDelta));
            _signalBus.Handle(new CharacterMoveSignal(moveDelta));
            _speed += _speedDelta * Time.deltaTime;

            _speed = Mathf.Clamp(_speed, 0, 1);
            _signalBus.Handle(new CharacterSpeedSignal(_speed));
        }
    }
}