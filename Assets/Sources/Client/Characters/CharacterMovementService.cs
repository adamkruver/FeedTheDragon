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

        private Vector3 _direction;

        public CharacterMovementService(ICurrentPlayerService currentPlayerService, ISignalBus signalBus, Camera camera)
        {
            _camera = camera;
            _currentPlayerService = currentPlayerService;
            _signalBus = signalBus;
        }

        public void Update()
        {
            if (_currentPlayerService.Character is not Character character)
                return;

            if (Input.GetMouseButton(0) == false)
                return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            int layer = 1 << LayerMask.NameToLayer("Terrain"); //todo Move to constans

            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layer))
            {
                _direction = raycastHit.point - character.Position.CurrentPosition;
                _direction.y = 0;

                Vector3 moveDelta = character.Speed.Value * Time.deltaTime * _direction.normalized;
                    
                _signalBus.Handle(new CharacterMoveSignal(moveDelta));
            }
        }
    }
}