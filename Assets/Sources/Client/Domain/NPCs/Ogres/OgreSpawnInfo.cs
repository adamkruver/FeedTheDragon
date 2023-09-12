using UnityEngine;

namespace Sources.Client.Domain.NPCs.Ogres
{
    public class OgreSpawnInfo
    {
        public OgreSpawnInfo(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}