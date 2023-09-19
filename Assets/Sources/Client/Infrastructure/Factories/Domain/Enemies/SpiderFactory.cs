using Sources.Client.Domain.Enemies.Spiders;

namespace Sources.Client.Infrastructure.Factories.Domain.Enemies
{
    public class SpiderFactory : EnemyFactoryBase<Spider, SpiderSpawnInfo>
    {
        protected override Spider CreateEntity(int id) =>
            new Spider(id);
    }
}