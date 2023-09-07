using Sources.Client.Domain.Ingredients;
using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Ingredients
{
    public abstract class IngredientSpawnPointBase : SpawnerBase
    {
        public override IIngredientType Type { get; }
        public override Vector3 Position { get; }
        public override Color Color { get; }
        public override float Size { get; }
    }
}