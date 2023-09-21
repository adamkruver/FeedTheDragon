using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.NPCs.Dragons.Signals
{
    public class CreateDragonSignal : ISignal
    {
        public CreateDragonSignal(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}