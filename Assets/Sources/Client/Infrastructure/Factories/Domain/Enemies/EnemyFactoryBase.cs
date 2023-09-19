using Sources.Client.Domain;
using Sources.Client.Domain.Components;
using Sources.Client.Domain.Enemies;

namespace Sources.Client.Infrastructure.Factories.Domain.Enemies
{
    public abstract class EnemyFactoryBase
    {
        public Enemy Create(int id, IEnemyType enemyType, EnemySpawnInfo spawnInfo)
        {
            Enemy enemy = new Enemy(id, enemyType);
            AddAllComponents(enemy, spawnInfo);

            return enemy;
        }

        protected virtual void AddComponents(Enemy enemy, EnemySpawnInfo spawnInfo)
        {
        }

        protected void AddAllComponents(Enemy enemy, EnemySpawnInfo spawnInfo)
        {
            AddBaseComponents(enemy, spawnInfo);
            AddComponents(enemy, spawnInfo);
        }

        protected void AddBaseComponents(Enemy enemy, EnemySpawnInfo spawnInfo)
        {
            enemy.AddComponent(new PositionComponent(spawnInfo.Position));
            enemy.AddComponent(new VisibilityComponent(true));
        }
    }
}