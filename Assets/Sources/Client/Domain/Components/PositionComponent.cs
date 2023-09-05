using System;
using UnityEngine;

namespace Sources.Client.Domain.Components
{
    public class PositionComponent
    {
        public PositionComponent(Vector3 value)
        {
            Value = value;
        }

        public event Action Changed;
        
        public Vector3 Value { get; private set; }

        public void Set(Vector3 position)
        {
            Value = position;
        }

        public void Move(Vector3 signalMoveDelta)
        {
            Value += signalMoveDelta;

            Changed?.Invoke();
        }
    }
}