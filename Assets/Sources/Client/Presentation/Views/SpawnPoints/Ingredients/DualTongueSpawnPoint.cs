using Sources.Client.Domain.Ingredients.IngredientTypes;
using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Ingredients
{
    public class DualTongueSpawnPoint : SpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override object Type { get; } = new DualTongue();
        public override Vector3 Position => transform.position;
        public override Color Color { get; } = new Color(196 / 255f, 158 / 255f, 1);
    }
}