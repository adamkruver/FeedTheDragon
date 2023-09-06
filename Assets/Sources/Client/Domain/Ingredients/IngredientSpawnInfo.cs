using UnityEngine;

namespace Sources.Client.Domain.Ingredients
{
    public class IngredientSpawnInfo
    {
        public IngredientSpawnInfo(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}