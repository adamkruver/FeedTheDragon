using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Enemies;

namespace Sources.Client.Infrastructure.Factories.Domain.Enemies
{
    public abstract class EnemyFactoryBase<TEnemy, TSpawnInfo>
        where TEnemy : Composite, IEnemy
        where TSpawnInfo : IEnemySpawnInfo<TEnemy>

    {
        public TEnemy Create(int id, TSpawnInfo spawnInfo)
        {
            TEnemy enemy = CreateEntity(id);
            AddAllComponents(enemy, spawnInfo);

            return enemy;
        }

        protected abstract TEnemy CreateEntity(int id);

        protected virtual void AddComponents(TEnemy enemy, TSpawnInfo spawnInfo)
        {
        }

        protected void AddAllComponents(TEnemy enemy, TSpawnInfo spawnInfo)
        {
            AddBaseComponents(enemy, spawnInfo);
            AddComponents(enemy, spawnInfo);
        }

        protected void AddBaseComponents(TEnemy enemy, TSpawnInfo spawnInfo)
        {
            enemy.AddComponent(new PositionComponent(spawnInfo.Position));
            enemy.AddComponent(new VisibilityComponent(true));
        }
    }
}