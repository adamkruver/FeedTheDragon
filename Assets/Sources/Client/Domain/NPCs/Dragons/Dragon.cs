using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.NPCs.Dragons
{
    public class Dragon : Composite, IEntity, IEntityType
    {
        public Dragon(int id) => 
            Id = id; 
        
        public IEntityType EntityType => this;
        public int Id { get; }
    }
}