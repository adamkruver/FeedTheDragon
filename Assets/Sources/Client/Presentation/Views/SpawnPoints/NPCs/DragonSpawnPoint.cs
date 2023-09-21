using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.NPCs
{
    public class DragonSpawnPoint : SpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override object Type { get; } = default;
        public override Vector3 Position => transform.position;
        public override Color Color => new Color(0,0,0);
    }
}