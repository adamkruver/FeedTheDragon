using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Ingredients
{
    public class ToxicFrogSpawnPoint : IngredientSpawnPointBase
    {
        public override float Size { get; } = .5f;
        public override IIngredientType Type { get; } = new ToxicFrog();
        public override Vector3 Position => transform.position;
        public override Color Color { get; } = new Color(220f / 255f, 1f, 0);
    }
}