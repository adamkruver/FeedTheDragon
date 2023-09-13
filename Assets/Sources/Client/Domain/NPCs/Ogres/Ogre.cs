using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.NPCs.Ogres
{
    public class Ogre : Composite, IEntity
    {
        public Ogre(int id) =>
            Id = id;

        public int Id { get; }
    }
}