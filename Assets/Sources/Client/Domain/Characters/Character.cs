using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Characters
{
    public class Character : Composite, IEntity
    {
        public Character(int id) =>
            Id = id;

        public int Id { get; }
    }
}