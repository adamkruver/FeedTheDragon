using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Enemies
{
    public class Enemy : Composite, IEntity
    {
        public Enemy(int id, IEnemyType type)
        {
            Id = id;
            Type = type;
        }

        public int Id { get; }
        public IEnemyType Type { get; }
    }
}