using Sources.Client.Domain.Ingredients.IngredientTypes;
using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Ingredients
{
    public class EyeRootSpawnPoint : SpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override object Type { get; } = new EyeRoot();
        public override Vector3 Position => transform.position;
        public override Color Color { get; } = new Color(62 / 255f, 132 / 255f, 202 / 255f);
    }
}