using UnityEngine;

namespace Sources.Client.Domain.Characters
{
    public class PeasantSpawnInfo
    {
        public PeasantSpawnInfo(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}