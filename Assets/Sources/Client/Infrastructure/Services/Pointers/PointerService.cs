using System.Collections.Generic;
using Sources.Client.Frameworks.StateMachines;
using Sources.Client.InfrastructureInterfaces.Pointers;
using UnityEngine;

namespace Sources.Client.Infrastructure.Services.Pointers
{
    public class PointerService : IUpdatable
    {
        private readonly Dictionary<int, IPointerHandler> _handlers = new Dictionary<int, IPointerHandler>();
        private readonly Dictionary<int, bool> _touchFlags = new Dictionary<int, bool>();
        private readonly PointerUIService _pointerUIService = new PointerUIService();

        private IPointerUntouchedMoveHandler _pointerUntouchedMoveHandler;

        public void RegisterHandler(int pointerId, IPointerHandler handler)
        {
            _handlers[pointerId] = handler;
            _touchFlags[pointerId] = false;
        }

        public void RegisterUntouchedMoveHandler(IPointerUntouchedMoveHandler handler) => 
            _pointerUntouchedMoveHandler = handler;

        public void UnregisterAll()
        {
            _handlers.Clear();
            _pointerUntouchedMoveHandler = null;
            _touchFlags.Clear();
        }

        public void Update(float deltaTime)
        {
            Vector2 pointerPosition = Input.mousePosition;
            
            foreach (KeyValuePair<int, IPointerHandler> pair in _handlers)
                Handle(pair.Key, pair.Value, pointerPosition);

            if(IsTouched() == false)
                HandleUntouchedMove(pointerPosition);
        }

        private bool IsTouched()
        {
            foreach (bool flag in _touchFlags.Values)
                if (flag)
                    return true;

            return false;
        }

        private void Handle(int pointerId, IPointerHandler pointerHandler, Vector2 pointerPosition)
        {
            if (_touchFlags[pointerId] == false)
            {
                if (Input.GetMouseButtonDown(pointerId))
                    HandleTouchStart(pointerId, pointerHandler, pointerPosition);
            }
            else
            {
                if (Input.GetMouseButtonUp(pointerId))
                    HandleTouchEnd(pointerId, pointerHandler, pointerPosition);
                else
                    HandleTouchMove(pointerHandler, pointerPosition);
            }
        }

        private void HandleTouchStart(int pointerId, IPointerHandler pointerHandler, Vector2 pointerPosition)
        {
            if(_pointerUIService.IsPointerOverUI)
                return;
            
            pointerHandler.OnTouchStart(pointerPosition);
            _touchFlags[pointerId] = true;
        }

        private void HandleTouchEnd(int pointerId, IPointerHandler pointerHandler, Vector2 pointerPosition)
        {
            pointerHandler.OnTouchEnd(pointerPosition);
            _touchFlags[pointerId] = false;
        }

        private void HandleTouchMove(IPointerHandler pointerHandler, Vector2 pointerPosition) => 
            pointerHandler.OnMove(pointerPosition);

        private void HandleUntouchedMove(Vector2 pointerPosition)
        {
            if(_pointerUIService.IsPointerOverUI)
                return;
            
            _pointerUntouchedMoveHandler?.OnMove(pointerPosition);
        }
    }
}