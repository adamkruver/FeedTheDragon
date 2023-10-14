namespace Sources.Client.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; }
        IEntityType EntityType { get; }
    }
}