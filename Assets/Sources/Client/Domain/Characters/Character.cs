using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Characters
{
    public class Character : Composite, IEntity, IEntityType
    {
        public Character(int id) =>
            Id = id;

        public int Id { get; }
        public IEntityType EntityType => this;
    }
}