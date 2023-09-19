namespace Sources.Client.Domain.Enemies.Spiders
{
    public class Spider : Composite, IEnemy
    {
        public Spider(int id) =>
            Id = id;

        public int Id { get; }
    }
}