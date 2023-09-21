using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Enemies
{
    public class BearSpawnPoint : SpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override object Type { get; } = default;
        public override Vector3 Position => transform.position;
        public override Color Color => new Color(130,120,0);
    }
}