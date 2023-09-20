using UnityEngine;

namespace Sources.Client.Domain.NPCs.Dragons
{
    public class DragonSpawnInfo
    {
        public DragonSpawnInfo(Vector3 position)
        {
            Position = position;
        }
        
        public Vector3 Position { get; }
    }
}