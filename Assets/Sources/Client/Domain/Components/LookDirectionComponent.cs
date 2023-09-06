using System;
using UnityEngine;

namespace Sources.Client.Domain.Components
{
    public class LookDirectionComponent : IComponent
    {
        public LookDirectionComponent(Vector3 initialValue)
        {
            Value = initialValue;
        }

        public event Action Changed;

        public Vector3 Value { get; private set; }

        public void Set(Vector3 direction)
        {
            Value = direction;
            
            Changed?.Invoke();
        }
    }
}