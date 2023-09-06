using Sources.Client.Domain.Ingredients;
using Sources.Spawners;
using UnityEngine;

namespace Presentation.Resources.Spawners.Ingredients
{
    public abstract class IngredientSpawnPointBase : SpawnerBase
    {
        public override IIngredientType Type { get; }
        public override Vector3 Position { get; }
        public override Color Color { get; }
        public override float Size { get; }
    }
}