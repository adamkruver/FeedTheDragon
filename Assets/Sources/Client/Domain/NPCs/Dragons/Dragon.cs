using Sources.Client.Domain.Entities;

namespace Sources.Client.Domain.NPCs.Dragons
{
    public class Dragon : Composite, IEntity
    {
        public Dragon(int id) => 
            Id = id; 
        
        public int Id { get; }
    }
}