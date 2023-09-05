using Sources.Client.Controllers.Characters.SIgnals;
using Sources.Client.Domain.Characters;
using Sources.Client.InfrastructureInterfaces.Services.CurrentPlayer;
using Sources.Client.InfrastructureInterfaces.SignalBus;
using UnityEngine;

namespace Sources.Client.Characters
{
    public class CharacterMovementService
    {
        private CharacterController _characterController;

        private Camera _camera;
        private readonly ICurrentPlayerService _currentPlayerService;
        private readonly ISignalBus _signalBus;
        private readonly int _layer;

        private Vector3 _direction;

        private float _speed = 0;
        private float _speedDelta = 5f;

        public CharacterMovementService(ICurrentPlayerService currentPlayerService, ISignalBus signalBus, Camera camera)
        {
            _camera = camera;
            _currentPlayerService = currentPlayerService;
            _signalBus = signalBus;

            _layer = 1 << LayerMask.NameToLayer("Terrain"); //todo Move to constans
        }

        public void Update()
        {
            Debug.Log(_speed);
            
            if (_currentPlayerService.Character is not Character character)
                return;

            if (Input.GetMouseButton(0) == false)
            {
                _speed -= _speedDelta* Time.deltaTime;
                _speed = Mathf.Clamp(_speed, 0, 1);
                _signalBus.Handle(new CharacterSpeedSignal(_speed));

                return;
            }

            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit,
                    Mathf.Infinity, _layer) == false)
            {
                _speed -= _speedDelta* Time.deltaTime;
                _speed = Mathf.Clamp(_speed, 0, 1);
                _signalBus.Handle(new CharacterSpeedSignal(_speed));

                return;
            }

            _direction = raycastHit.point - character.Position.Value;
            _direction.y = 0;

            Vector3 moveDelta = character.Speed.Value * Time.deltaTime * _direction.normalized;

            _signalBus.Handle(new CharacterRotateSignal(moveDelta));
            _signalBus.Handle(new CharacterMoveSignal(moveDelta));
            _speed += _speedDelta* Time.deltaTime;


            _speed = Mathf.Clamp(_speed, 0, 1);
            _signalBus.Handle(new CharacterSpeedSignal(_speed));
        }
    }
}