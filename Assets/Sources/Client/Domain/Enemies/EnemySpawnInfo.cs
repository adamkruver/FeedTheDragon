using UnityEngine;

namespace Sources.Client.Domain.Enemies
{
    public class EnemySpawnInfo
    {
        public EnemySpawnInfo(Vector3 position) =>
            Position = position;

        public Vector3 Position { get; }
    }
}