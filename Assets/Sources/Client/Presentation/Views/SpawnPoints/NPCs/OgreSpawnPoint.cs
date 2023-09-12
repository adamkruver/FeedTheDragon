using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.NPCs
{
    public class OgreSpawnPoint : SpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override object Type { get; } = default;
        public override Vector3 Position => transform.position;
        public override Color Color { get; } = new Color(0.2f, 165 / 255f, 0);
    }
}