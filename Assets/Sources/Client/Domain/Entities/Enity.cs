namespace Sources.Client.Domain.Entities
{
    public abstract class Enity
    {
        public Enity(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}