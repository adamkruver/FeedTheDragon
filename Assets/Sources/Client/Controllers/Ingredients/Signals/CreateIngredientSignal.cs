using Sources.Client.Domain.Ingredients;
using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.Signals
{
    public class CreateIngredientSignal : ISignal
    {
        public CreateIngredientSignal(IIngredientType type, Vector3 position)
        {
            Type = type;
            Position = position;
        }

        public IIngredientType Type { get; }
        public Vector3 Position { get; }
    }
}