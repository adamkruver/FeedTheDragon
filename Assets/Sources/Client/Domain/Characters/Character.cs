using Sources.Client.Domain.Components;
using Sources.Client.Domain.Entities;
using UnityEditor;

namespace Sources.Client.Domain.Characters
{
    public class Character : Enity
    {
        public readonly LookDirectionComponent LookDirection;
        public readonly PositionComponent Position;
        public readonly SpeedComponent Speed;

        public Character(int id, LookDirectionComponent lookDirection, PositionComponent position, SpeedComponent speed) : base(id)
        {
            LookDirection = lookDirection;
            Position = position;
            Speed = speed;
        }
    }
}