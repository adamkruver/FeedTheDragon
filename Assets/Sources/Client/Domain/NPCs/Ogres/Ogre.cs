using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.NPCs.Ogres
{
    public class Ogre : Composite, IEntity, IEntityType
    {
        public Ogre(int id) =>
            Id = id;

        public IEntityType EntityType => this;
        public int Id { get; }
    }
}