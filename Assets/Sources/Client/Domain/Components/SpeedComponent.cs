using System;

namespace Sources.Client.Domain.Components
{
    public class SpeedComponent : IComponent
    {
        private readonly float _multiplier;
        
        private float _value;

        public SpeedComponent(float multiplier) =>
            _multiplier = multiplier;

        public event Action Changed;

        public float Value => _value * _multiplier;

        public void Set(float speed)
        {
            _value = speed;
            
            Changed?.Invoke();
        }
    }
}