using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Enemies
{
    public class SpiderSpawnPoint : SpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override object Type { get; } = default;
        public override Vector3 Position => transform.position;
        public override Color Color { get; } = new Color(51f/255f, 54f/255f, 57f / 255f);
    }
}