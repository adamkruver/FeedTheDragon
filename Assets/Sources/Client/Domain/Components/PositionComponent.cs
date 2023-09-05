using System;
using UnityEngine;

namespace Sources.Client.Domain.Components
{
    public class PositionComponent
    {
        public PositionComponent(Vector3 currentPosition)
        {
            CurrentPosition = currentPosition;
        }

        public event Action Changed;
        
        public Vector3 CurrentPosition { get; private set; }

        public void Move(Vector3 signalMoveDelta)
        {
            CurrentPosition += signalMoveDelta;

            Changed?.Invoke();
        }
    }
}