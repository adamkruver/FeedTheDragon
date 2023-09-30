using Sources.Client.Frameworks.StateMachines;
using Sources.Client.Infrastructure.Services.Pointers.Handlers;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Pointers
{
    public class PointerService : IUpdatable
    {
        private IPointerHandler _pointerHandler;
        private const int PointerButton = 0;

        private readonly PointerUIService _pointerUIService = new PointerUIService();

        private bool _isPressed;

        public void Register(IPointerHandler pointerHandler) =>
            _pointerHandler = pointerHandler;
        
        public void Unregister(IPointerHandler pointerHandler) =>
            _pointerHandler = null;

        public void Update(float deltaTime)
        {
            if (_pointerHandler is null)
                return;

            Vector3 position = Input.mousePosition;

            if (Input.GetMouseButtonUp(PointerButton))
            {
                _isPressed = false;
                _pointerHandler.OnFinish(position);

                return;
            }

            if (Input.GetMouseButtonDown(PointerButton))
            {
                if (_pointerUIService.IsPointerOverUI)
                    return;

                _isPressed = true;
                _pointerHandler.OnStart(position);

                return;
            }

            if (Input.GetMouseButton(PointerButton))
            {
                if (_isPressed)
                    _pointerHandler.OnMove(position);
            }
        }
    }
}