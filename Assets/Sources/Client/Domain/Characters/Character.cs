using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.Characters
{
    public class Character : Composite, IEntity
    {
        private readonly int _id;

        public Character(int id) =>
            _id = id;

        public int Id { get; }
    }
}