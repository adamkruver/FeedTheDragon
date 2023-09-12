using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints
{
    public abstract class SpawnPointBase : MonoBehaviour
    {
        public abstract object Type { get; }
        public abstract Vector3 Position { get; }
        public abstract Color Color { get; }
        public abstract float Size { get; }
    }
}