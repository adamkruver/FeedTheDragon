using Sources.Client.InfrastructureInterfaces.SignalBus.Signals;
using UnityEngine;

namespace Sources.Client.Controllers.NPCs.Ogres.Signals
{
    public class CreateOgreSignal : ISignal
    {
        public CreateOgreSignal(Vector3 position) =>
            Position = position;

        public Vector3 Position { get; }
    }
}