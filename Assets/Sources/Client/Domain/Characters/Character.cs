using Sources.Client.Domain.Components;
using Sources.Client.Domain.Entities;
using UnityEditor;

namespace Sources.Client.Domain.Characters
{
    public class Character : Enity
    {
        public readonly DirectionComponent Direction;
        public readonly PositionComponent Position;
        public readonly SpeedComponent Speed;

        public Character(int id, DirectionComponent direction, PositionComponent position, SpeedComponent speed) : base(id)
        {
            Direction = direction;
            Position = position;
            Speed = speed;
        }
    }
}