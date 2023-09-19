using UnityEngine;

namespace Sources.Client.Domain.Enemies
{
    public interface IEnemySpawnInfo<T> where T: IEnemy
    {
        Vector3 Position { get; }
    }
}