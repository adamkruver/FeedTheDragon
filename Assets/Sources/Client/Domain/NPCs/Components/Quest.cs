using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.NPCs.Components
{
    public class Quest : Composite, IEntity, IComponent
    {
        public Quest(int id)
        { 
            Id = id;
        }

        public int Id { get; }
    }
}