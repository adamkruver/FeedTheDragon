using UnityEngine;

namespace Sources.Client.Domain.NPCs.Bears
{
    public class BearSpawnInfo
    {
        public BearSpawnInfo(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}