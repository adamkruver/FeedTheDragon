using Sources.Client.Domain.Ingredients;
using Sources.Client.Domain.Ingredients.IngredientTypes;
using UnityEngine;

namespace Sources.Client.Presentation.Views.SpawnPoints.Ingredients
{
    public class ChanterelleSpawnPoint : IngredientSpawnPointBase
    {
        public override float Size { get; } = 0.5f;
        public override IIngredientType Type { get; } = new Chanterelle();
        public override Vector3 Position => transform.position;
        public override Color Color { get; } = new Color(1, 165 / 255f, 0);
    }
}