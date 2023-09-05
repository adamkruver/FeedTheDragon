using Sources.Client.Domain.Entities;

namespace Sources.Client.InfrastructureInterfaces.Repositories
{
    public interface IEntityRepository
    {
        void Add(Enity entity);
        
        Enity Get(int id);
    }
}