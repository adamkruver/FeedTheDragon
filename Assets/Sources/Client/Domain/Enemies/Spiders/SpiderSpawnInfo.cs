using UnityEngine;

namespace Sources.Client.Domain.Enemies.Spiders
{
    public class SpiderSpawnInfo : IEnemySpawnInfo<Spider>
    {
        public SpiderSpawnInfo(Vector3 position) =>
            Position = position;

        public Vector3 Position { get; }
    }
}