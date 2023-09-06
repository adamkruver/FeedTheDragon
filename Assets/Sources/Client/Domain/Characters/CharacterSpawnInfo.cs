using UnityEngine;

namespace Sources.Client.Domain.Characters
{
    public class CharacterSpawnInfo
    {
        public CharacterSpawnInfo(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}