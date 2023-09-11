using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.Ingredients.Signals
{
    public class ToxicFrogJumpSignal : ISignal
    {
        public ToxicFrogJumpSignal(int id, Vector3 destination, float movementSpeed)
        {
            Id = id;
            Destination = destination;
            MovementSpeed = movementSpeed;
        }

        public int Id { get; }
        public Vector3 Destination { get; }
        public float MovementSpeed { get; }
    }
}