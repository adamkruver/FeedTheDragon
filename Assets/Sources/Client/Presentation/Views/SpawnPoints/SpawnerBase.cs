using Sources.Client.Domain.Ingredients;
using UnityEngine;

namespace Sources.Spawners
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        public abstract IIngredientType Type { get; }
        public abstract Vector3 Position { get; }
        public abstract Color Color { get; }
        public abstract float Size { get; }
    }
}